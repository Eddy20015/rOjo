using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLeg : MonoBehaviour
{
    [SerializeField] Transform foot, spiderPlatform, legTop, legBottom, look;

    [SerializeField] float angleOffset, moveSpeed, distance, positionOffset, initialOffset, legContraction;

    [SerializeField] Vector3 initialFootPosition, raycastDirection;

    [SerializeField] LayerMask layerMask;

    [SerializeField] GameObject hit;

    [SerializeField] Spider spider;

    [SerializeField] bool backLeg;

    bool moving;

    ContactFilter2D contactFilter2D;

    Vector3 footPosition;

    float currentFootPosition;

    // Start is called before the first frame update
    void Start()
    {
        SetFoot();

        currentFootPosition += initialOffset;

        foot.transform.position = (Vector2)spiderPlatform.transform.position +
            spider.CalculatePosition(currentFootPosition, false);

        contactFilter2D = new();

        contactFilter2D.useTriggers = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = GetFootPosition(spider.GetPosition() + positionOffset);

        if (!moving)
        {
            foot.transform.position = footPosition;

            if (!backLeg)
            {
                if (Vector2.Distance(footPosition, newPosition) > distance)
                {
                    SetFoot();
                    StartCoroutine(MoveLeg(foot.position, GetFootPosition(currentFootPosition)));
                }
            } else
            {
                if (Vector2.Distance(transform.position, foot.transform.position) > 3.75f)
                {
                    SetFoot();
                    StartCoroutine(MoveLeg(foot.position, GetFootPosition(currentFootPosition)));
                }
            }
        }

        float legDistance = Vector3.Distance(transform.position, foot.transform.position);

        Vector3 direction = foot.transform.position - transform.position;

        legTop.transform.localRotation = Quaternion.Euler(0, 0, legDistance * legContraction);
        legBottom.transform.localRotation = Quaternion.Euler(0, 0, -legDistance * legContraction);

        //look.transform.LookAt(foot);

        //look.transform.localRotation = Quaternion.Euler(look.transform.localEulerAngles.x, look.transform.localEulerAngles.y, 0);
    }

    void Raycast()
    {
        RaycastHit2D h = Physics2D.Raycast(transform.position, raycastDirection, 100, layerMask);

        if (h.collider != null)
        {
            bool canMove = Vector2.Distance(foot.position, h.point) > distance || foot.transform.localPosition.y < 0
                || Vector3.Distance(transform.localPosition, foot.transform.localPosition) > 4.1f;

            if (canMove && !moving)
            {
                StartCoroutine(MoveLeg(foot.position, h.point));
            }
        }
    }

    void CalculatePosition()
    {
        if (positionOffset > 0)
        {
            if (((spider.GetPosition() - currentFootPosition) > 2 || (spider.GetPosition() + spider.GetCircumference() - currentFootPosition) > 2)
            && !moving)
            {
                SetFoot();
                StartCoroutine(MoveLeg(foot.position, (Vector2)spider.transform.position + spider.CalculatePosition(currentFootPosition, false)));
            }
        } else
        {
            if (((spider.GetPosition() - currentFootPosition) < -3 || (spider.GetPosition() + spider.GetCircumference() - currentFootPosition) < -3)
            && !moving)
            {
                SetFoot();
                StartCoroutine(MoveLeg(foot.position, (Vector2)spider.transform.position + spider.CalculatePosition(currentFootPosition, false)));
            }
        }
    }

    void SetFoot()
    {
        currentFootPosition = spider.GetPosition() + positionOffset + initialOffset;
    }

    Vector2 GetFootPosition(float f)
    {
        return (Vector2)spiderPlatform.transform.position + spider.CalculatePosition(f, false);
    }

    IEnumerator MoveLeg(Vector2 start, Vector2 end)
    {
        moving = true;

        // hit gameobject so I can tell where the raycast hits
        //GameObject h = Instantiate(hit);

        //h.transform.position = end;

        float f = 0;

        // interpolates leg position
        // speed = 1 would move the leg in 1 second, speed = 0.5f would move the leg in 2 seconds, etc.
        while (f < 1)
        {
            f += Time.deltaTime * moveSpeed;

            if (f > 1)
            {
                f = 1;
            }

            // moves foot
            foot.transform.position = Vector2.Lerp(start, end, f);

            // adds height
            //foot.transform.position += Mathf.Sin(f * Mathf.PI) * spider.transform.up;
            yield return new WaitForEndOfFrame();
        }

        footPosition = foot.transform.position;

        //Destroy(h);

        moving = false;
    }
}
