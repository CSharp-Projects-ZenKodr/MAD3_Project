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
    private float maxSpeed = 60;
    private float minSpeed = 20;
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

    public ParticleSystem electricExplo;
    bool particleSystemPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isGameRunning)
        {
            return;
        }

        // Gets the player moveing forward.
        direction.z = speed;

        checkInput();

        // Calculate where we should be in the future

        Vector3 targetPostition = transform.position.z * transform.forward + transform.position.y * transform.up;

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

        Vector3 diff = targetPostition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

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
        if(particleSystemPlayed == true)
        {
            electricExplo.Stop();
            particleSystemPlayed = false;
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

    // Jump method increases the characters Y direction simulating a jump
    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Obstacles")
        {
            if (particleSystemPlayed == false) {
                electricExplo.Play();
                particleSystemPlayed = true;
            }
            
            Destroy(hit.gameObject);
            speed = minSpeed;
        }
    }
}
