using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    [Range(0f, 1f)]
    [Tooltip("0 means that it will basically not move at all and 1 means it will follow the camera exactly. Use large values for far things")]
    [SerializeField] private float parallaxEffectX;
    [Range(0f, 1f)]
    [SerializeField] private float parallaxEffectY;

    [SerializeField] GameObject cam;
    [SerializeField] bool isLooping;

    private float startX, startY, length;


    void Start()
    {
        if (isLooping)
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        startX = transform.position.x;
        startY = transform.position.y;
    }

    void Update()
    {
        float distX = (cam.transform.position.x * parallaxEffectX);
        float distY = (cam.transform.position.y * parallaxEffectY);
        transform.position = new Vector3(startX + distX, startY + distY, transform.position.z);

        if (isLooping) LoopImage();
    }

    private void LoopImage()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffectX));
        if (temp > startX + length) startX += length;
        else if (temp < startX - length) startX -= length;
    }
}
