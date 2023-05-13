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

        if (spiderPosition > circumference)
        {
            spiderPosition -= circumference;
        }

        transform.localPosition = CalculatePosition(spiderPosition);

        body.transform.SetLocalPositionAndRotation(move * Mathf.Sin(moveTime) * Vector3.up,
            Quaternion.Euler(rotate * Mathf.Sin(rotateTime) * Vector3.forward));
    }

    void CalculateCircumference()
    {
        circumference = (width * 2) + (height * 2);
    }

    public Vector2 CalculatePosition(float f)
    {
        // this is about to be the most 1 AM code ever


        return new();
    }
}
