using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField] Transform body, platform;

    [SerializeField] float move, rotate, moveSpeed, rotateSpeed;

    float moveTime, rotateTime, width, height, circumference, spiderPosition;

    // Start is called before the first frame update
    void Start()
    {
        width = platform.transform.localScale.x;
        height = platform.transform.localScale.y;

        CalculateCircumference();
    }

    // Update is called once per frame
    void Update()
    {
        moveTime += Time.deltaTime * moveSpeed;
        rotateTime += Time.deltaTime * rotateSpeed;

        spiderPosition += Time.deltaTime;

        transform.localPosition = CalculatePosition();

        body.transform.SetLocalPositionAndRotation(move * Mathf.Sin(moveTime) * Vector3.up,
            Quaternion.Euler(rotate * Mathf.Sin(rotateTime) * Vector3.forward));
    }

    void CalculateCircumference()
    {
        circumference = (width * 2) + (height * 2);
    }

    Vector2 CalculatePosition()
    {
        if (spiderPosition > circumference)
        {
            spiderPosition -= circumference;
        }

        return new();
    }
}
