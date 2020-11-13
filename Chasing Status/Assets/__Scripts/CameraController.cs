using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
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
