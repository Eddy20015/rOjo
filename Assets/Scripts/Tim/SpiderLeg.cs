using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLeg : MonoBehaviour
{
    [SerializeField] Transform foot, spider;

    [SerializeField] float angleOffset, moveSpeed;

    [SerializeField] Vector3 initialFootPosition, raycastDirection;

    [SerializeField] LayerMask layerMask;

    [SerializeField] GameObject hit;

    bool moving;

    ContactFilter2D contactFilter2D;

    Vector3 footPosition;

    // Start is called before the first frame update
    void Start()
    {
        foot.transform.position = spider.transform.position + initialFootPosition;

        contactFilter2D = new();

        contactFilter2D.useTriggers = false;
    }

    // Update is called once per frame
    void Update()
    {
        Raycast();

        if (!moving)
        {
            foot.transform.position = footPosition;
        }
    }

    void Raycast()
    {
        RaycastHit2D h = Physics2D.Raycast(transform.position, raycastDirection, 100, layerMask);

        if (h.collider != null)
        {
            if (Vector2.Distance(foot.position, h.point) > 2 && !moving)
            {
                StartCoroutine(MoveLeg(foot.position, h.point));
            }
        }
    }

    IEnumerator MoveLeg(Vector2 start, Vector2 end)
    {
        moving = true;

        // hit gameobject so I can tell where the raycast hits
        GameObject h = Instantiate(hit);

        h.transform.position = end;

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

        Destroy(h);

        moving = false;
    }
}
