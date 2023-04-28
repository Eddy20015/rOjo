using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxScript : MonoBehaviour
{
    [Range(0f, 1f)]
    [Tooltip("0 means that it will basically not move at all and 1 means it will follow the camera exactly. Use large values for far things")]
    [SerializeField] private float paralaxEffectX;
    [Range(0f, 1f)]
    [SerializeField] private float paralaxEffectY;

    [SerializeField] GameObject cam;
    [SerializeField] private float length = 1f;
    private Vector3 lastCamPos;
    private float startX, startY;


    void Start()
    {
        lastCamPos = cam.transform.position;
        startX = transform.position.x;
        startY = transform.position.y;
    }

    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - paralaxEffectX));
        float distX = (cam.transform.position.x * paralaxEffectX);
        float distY = (cam.transform.position.y * paralaxEffectY);
        transform.position = new Vector3(startX + distX, startY + distY, transform.position.z);

        //if (temp > startX + length) startX += length;
        //else if (temp < startX - length) startX -= length;
    }
}
