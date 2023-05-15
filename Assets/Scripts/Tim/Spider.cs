using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField] Transform body, platform;

    [SerializeField] float move, rotate, moveSpeed, rotateSpeed, spiderMoveSpeed, spiderRotationSpeed;

    float moveTime, rotateTime, width, height, circumference, spiderPosition, targetRotation, oldRotation, rotationLerp;

    Quaternion oldSpiderRotation;

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

        spiderPosition += spiderMoveSpeed * Time.deltaTime;

        if (spiderPosition > circumference)
        {
            spiderPosition -= circumference;
        }

        if (rotationLerp < 1)
        {
            rotationLerp += spiderRotationSpeed * Time.deltaTime;
        } else
        {
            rotationLerp = 1;
        }

        transform.SetLocalPositionAndRotation(CalculatePosition(spiderPosition, true),
            Quaternion.Lerp(oldSpiderRotation, Quaternion.Euler(0,0,targetRotation), rotationLerp));

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

            /*if (remainder < (height + 1) && isSpider)
            {
                targetRotation = 90;
            }*/
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

            /*if (remainder < (width + 1) && isSpider)
            {
                targetRotation = 0;
            }*/

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

            /*if (remainder < (height + 1) && isSpider)
            {
                targetRotation = 270;
            }*/
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

        if (oldRotation != targetRotation)
        {
            oldRotation = targetRotation;
            oldSpiderRotation = transform.localRotation;
            rotationLerp = 0;
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
