using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    public Transform target; // Sets the tranform of the target (player)
    private Vector3 offset; // Sets the cameras offset from the target (player)

    // Start is called before the first frame update
    void Start()
    {
        // Sets offset of the camera from the players position
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Create a new Position based on the offset
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
        // Apply new position
        transform.position = newPosition;
    }
}
