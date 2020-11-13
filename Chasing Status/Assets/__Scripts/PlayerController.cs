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

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = speed;

        if(controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        } else
        {
            direction.y += gravity * Time.deltaTime;
        }

        // Gather the inputs on which lane we are in
        if (Input.GetKeyDown(KeyCode.D))
        {
            desiredLane++;
            if (desiredLane == 3) desiredLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            desiredLane--;
            if (desiredLane == -1) desiredLane = 0;
        }

        // Calculate where we should be in the future

        Vector3 targetPostition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane == 0)
        {
            targetPostition += Vector3.left * laneDistance;
        } else if(desiredLane == 2)
        {
            targetPostition += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPostition, 80 * Time.fixedDeltaTime);

    }

    // FixedUpdate called once per frame
    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }
}
