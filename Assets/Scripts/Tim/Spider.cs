using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField] Transform body, platform;

    [SerializeField] float bodyMoveStrength, bodyRotateStrength, bodyMoveSpeed, bodyRotateSpeed, spiderMoveSpeed, spiderRotateSpeed;

    [SerializeField] SpriteRenderer eye;

    [SerializeField] Sprite eyeClosed, eyeOpen;

    float bodyMoveTime, bodyRotateTime, width, height, circumference, spiderPosition, targetRotation, oldRotation, rotationLerp;

    Quaternion oldSpiderRotation;

    bool moving, seeing;

    [Header("Sounds")]
    [SerializeField] AK.Wwise.Event startDefault;
    [SerializeField] AK.Wwise.Event stopDefault;
    [SerializeField] AK.Wwise.Event startIntensified;
    [SerializeField] AK.Wwise.Event stopIntensified;
    public static int numDefaultSound = 0;
    public static int numIntensifiedSound = 0;
    private bool playingDefaultSound = false;
    private bool playingIntensifiedSound = false;

    // Start is called before the first frame update
    void Start()
    {
        moving = true;

        width = platform.transform.localScale.x;
        height = platform.transform.localScale.y;

        CalculateCircumference();
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            return;
        }

        Rotate();

        bodyMoveTime += Time.deltaTime * bodyMoveSpeed;
        bodyRotateTime += Time.deltaTime * bodyRotateSpeed;

        spiderPosition += spiderMoveSpeed * Time.deltaTime;

        if (spiderPosition > circumference)
        {
            spiderPosition -= circumference;
        }

        if (rotationLerp < 1)
        {
            rotationLerp += spiderRotateSpeed * Time.deltaTime;
        } else
        {
            rotationLerp = 1;
        }

        float lerp = (1 - Mathf.Cos(rotationLerp * Mathf.PI)) / 2;

        transform.SetLocalPositionAndRotation(CalculatePosition(spiderPosition, true),
            Quaternion.Lerp(oldSpiderRotation, Quaternion.Euler(0,0,targetRotation), lerp));

        body.transform.SetLocalPositionAndRotation(bodyMoveStrength * Mathf.Sin(bodyMoveTime) * Vector3.up,
            Quaternion.Euler(bodyRotateStrength * Mathf.Sin(bodyRotateTime) * Vector3.forward));
    }

    public void SetMoving(bool b)
    {
        moving = b;
    }

    public void SetSeeing(bool b)
    {
        seeing = b;
        eye.sprite = b ? eyeOpen : eyeClosed;

        if (!playingDefaultSound)
        {
            if (numDefaultSound == 0)
            {
                startDefault.Post(gameObject);
                //print("start default");
            }
            playingDefaultSound = true;
            numDefaultSound++;
            //print("playing default = " + numDefaultSound);
        }
        else
        {
            numDefaultSound--;
            playingDefaultSound = false;
            if (numDefaultSound == 0)
            {
                stopDefault.Post(gameObject);
                //print("stop default");
            }
            //print("playing default = " + numDefaultSound);
        }

        if (!playingIntensifiedSound)
        {
            if (numIntensifiedSound == 0)
            {
                startIntensified.Post(gameObject);
                //print("start intensified");
            }
            numIntensifiedSound++;
            playingIntensifiedSound = true;
            //print("playing intensified = " + numIntensifiedSound);
        }
        else
        {
            numIntensifiedSound--;
            playingIntensifiedSound = false;
            if (numIntensifiedSound == 0)
            {
                stopIntensified.Post(gameObject);
                //print("stop intensified");
            }
            //print("playing intensified = " + numIntensifiedSound);
        }

    }

    void CalculateCircumference()
    {
        circumference = (width * 2) + (height * 2);
    }

    void Rotate()
    {
        float remainder = spiderPosition + 1;

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
        }
        else
        {
            targetRotation = 90;
            remainder = 0;
        }

        if (remainder > width)
        {
            remainder -= width;

        }
        else if (remainder > 0)
        {
            targetRotation = 0;
            remainder = 0;
        }

        if (remainder > height)
        {
            remainder -= height;
        }
        else if (remainder > 0)
        {
            targetRotation = 270;
            remainder = 0;
        }

        if (remainder > 0)
        {
            targetRotation = 180;
        }

        if (oldRotation != targetRotation)
        {
            oldRotation = targetRotation;
            oldSpiderRotation = transform.localRotation;
            rotationLerp = 0;
        }
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

            /*if (isSpider)
            {
                targetRotation = 90;
            }*/
        }

        if (remainder > width)
        {
            remainder -= width;
            v += width * Vector2.right;

        } else if (remainder > 0)
        {
            v += remainder * Vector2.right;
            remainder = 0;

            /*if (isSpider)
            {
                targetRotation = 0;
            }*/
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

            /*if (isSpider)
            {
                targetRotation = 270;
            }*/
        }

        v += remainder * Vector2.left;

        /*if (remainder > 0 && isSpider)
        {
            targetRotation = 180;
        }*/

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

    public bool GetSeeing()
    {
        return seeing;
    }
}
