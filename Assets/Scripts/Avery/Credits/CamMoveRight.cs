using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveRight : MonoBehaviour
{
    [SerializeField] float speed = 1.5f;

    // Update is called once per frame
    void Update()
    {
        // Calculate the new position of the camera
        Vector3 newPosition = transform.position + Vector3.right * speed * Time.deltaTime;

        // Update the camera's position
        transform.position = newPosition;
    }
}
