using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    private Vector3 direction;

    // Set the desired lane to the middle: left=1, middle=1, right=2
    private int desiredLane = 1;
    // Maximum and Minimum speeds
    private float maxSpeed = 50;
    private float minSpeed = 10;
    // Rate of which the player character drops back down to the ground
    private int dropSpeed = 20;

    // Character movement speed
    [SerializeField]
    public float speed;
    [SerializeField]
    public float jumpForce;
    [SerializeField]
    public float gravity = -20;

    // The distance between each lane
    [SerializeField]
    public float laneDistance = 4;

    // Particle reference
    public ParticleSystem electricExplo;
    
    // Booleans for particle and player invulnerability
    bool particleSystemPlayed = false;
    bool invulnerable = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get player Controller
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the games no longer running return
        if (!GameManager.isGameRunning)
        {
            return;
        }

        // Gets the player moveing forward.
        direction.z = speed;

        // Check for input
        checkInput();

        // Calculate where we should be in the future
        Vector3 targetPostition = transform.position.z * transform.forward + transform.position.y * transform.up;

        // If statements decide if the player can move into which lanes
        if(desiredLane == 0)
        {
            targetPostition += Vector3.left * laneDistance;
        } else if(desiredLane == 2)
        {
            targetPostition += Vector3.right * laneDistance;
        }
        
        if (transform.position == targetPostition)
        {
            return;
        }

        // Sets the player direction with a difference 
        Vector3 diff = targetPostition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

        // Ensure smooth movement 
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        } else
        {
            controller.Move(diff);
        }

    }

    // FixedUpdate called once per frame
    private void FixedUpdate()
    {
        // Checks for player invulnerability
        if (invulnerable)
        {
            // Makes a quick timer
            float timer = 10;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            invulnerable = false;

        }

        if (!GameManager.isGameRunning)
        {
            return;
        }
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void checkInput()
    {
        // Checks if the player is on the ground and has press the spacebar to Jump
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
            Jump();
        // Applies gravity over time to bring the player back down to the ground
        else
            direction.y += gravity * Time.deltaTime;

        // If D is pressed increase the desiredLane and reset if over the limit
        if (Input.GetKeyDown(KeyCode.D))
            desiredLane++;
            if (desiredLane == 3) desiredLane = 2;
        // If A is pressed decrease the desiredLane and reset if under the limit
        if (Input.GetKeyDown(KeyCode.A))
            desiredLane--;
            if (desiredLane == -1) desiredLane = 0;


        // If W is pressed increase the players speed up and never above maxSpeed
        if (Input.GetKeyDown(KeyCode.W) && controller.isGrounded)
            speed = speed * 2;
            if (speed >= maxSpeed)
                speed = maxSpeed;

        // If S is pressed decrease the players speed down by half and never below minSpeed
        if (Input.GetKeyDown(KeyCode.S) && controller.isGrounded)
            speed = speed / 2;
            if (speed <= minSpeed)
                speed = minSpeed;

        // If S is pressed and player is not ground slam the player back down to the ground
        if (!controller.isGrounded && Input.GetKeyDown(KeyCode.S))
        {
            // Multiples the y direction with gravity and a dropSpeed to bring player down, using Time.deltaTime for smoothing
            direction.y += (gravity * dropSpeed) * Time.deltaTime;
            Debug.Log("Put me down!");
        }
    
    }

    /**
     * Jump function to increase the characters Y direction simulating a jump
     */
    private void Jump()
    {
        direction.y = jumpForce;
    }

    /**
     * On Collision function that registers what hit the player
     */
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // If what the player collided with was an Obstacle do..
        if (hit.gameObject.tag == "Obstacles")
        {
            FindObjectOfType<AudioManager>().PlaySound("CollisionEffect");
            // Start the Coroutine function PlayParticle passing electricExplo
            StartCoroutine(PlayParticle(electricExplo));
            // Destory the object the player hit
            Destroy(hit.gameObject);
            
            if (invulnerable == false) 
                speed = minSpeed; // Set player speed back to minimum speed

        } else if (hit.gameObject.tag == "PowerUp")
        {
            FindObjectOfType<AudioManager>().PlaySound("CollisionEffect");
            // Start the Coroutine function PlayParticle passing electricExplo
            StartCoroutine(PlayParticle(electricExplo));
            // Destory the object the player hit
            Destroy(hit.gameObject);
            // Set player speed back to maximum speed x 2
            speed = maxSpeed * 2;
            // Makes the player invulnerable
            invulnerable = true;
        }
    }

    /**
     * Coroutine Function to play a passed ParticleSystem
     */
    IEnumerator PlayParticle(ParticleSystem part)
    {
        // If the ParticleSystem is NOT playing then Play
        if (!part.isPlaying)
        {
            part.Play();
            yield return null;
        } 
        // If the ParticleSystem IS playing then Stop waiting .1 of a second
        if (part.isPlaying)
        {
            part.Stop();
            yield return new WaitForSeconds(.1f);
        }
    }

}
