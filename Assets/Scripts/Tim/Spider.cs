using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField] Transform body, platform;

    [SerializeField] float move, rotate, moveSpeed, rotateSpeed;

    float moveTime, rotateTime, width, height, circumference, spiderPosition, targetRotation;

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

        transform.SetLocalPositionAndRotation(CalculatePosition(spiderPosition, true),
            Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0,0,targetRotation), Time.deltaTime));

        body.transform.SetLocalPositionAndRotation(move * Mathf.Sin(moveTime) * Vector3.up,
            Quaternion.Euler(rotate * Mathf.Sin(rotateTime) * Vector3.forward));
    }

    void CalculateCircumference()
    {
        circumference = (width * 2) + (height * 2);
    }

    public Vector2 CalculatePosition(float f, bool isSpider)
    {
        // this is about to be the most 1 AM code ever
        Vector2 v = new();

        float remainder = f;

        if (remainder > circumference)
        {
            remainder -= circumference;
        }

        if (remainder < 0)
        {
            remainder += circumference;
        }

        if (remainder > height)
        {
            remainder -= height;
            v += height * Vector2.up;
        }
        else
        {
            v += remainder * Vector2.up;
            remainder = 0;

            if (isSpider)
            {
                targetRotation = 90;
            }
        }

        if (remainder > width)
        {
            remainder -= width;
            v += width * Vector2.right;
        } else if (remainder > 0)
        {
            v += remainder * Vector2.right;
            remainder = 0;

            if (isSpider)
            {
                targetRotation = 0;
            }
        }

        if (remainder > height)
        {
            remainder -= height;
            v += height * Vector2.down;
        }
        else if (remainder > 0)
        {
            v += remainder * Vector2.down;
            remainder = 0;

            if (isSpider)
            {
                targetRotation = 270;
            }
        }

        v += remainder * Vector2.left;

        if (remainder > 0 && isSpider)
        {
            targetRotation = 180;
        }

        return v + new Vector2(-width / 2, -height / 2);
    }

    public float CalculateRotation(float f)
    {
        return (f * -360 / circumference) + 90;
    }

    public float GetPosition()
    {
        return spiderPosition;
    }

    public float GetCircumference()
    {
        return circumference;
    }
}
