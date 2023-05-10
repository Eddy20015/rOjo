using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLeg : MonoBehaviour
{
    [SerializeField] Transform foot;

    [SerializeField] float angleOffset, moveSpeed;

    [SerializeField] Vector2 initialFootPosition;

    bool moving;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Raycast();
    }

    void Raycast()
    {
        RaycastHit2D h = Physics2D.Raycast(transform.position, transform.right);

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
        float f = 0;

        // interpolates leg position
        // speed = 1 would move the leg in 1 second, speed = 0.5f would move the leg in 2 seconds, etc.
        while (f < 1)
        {
            yield return new WaitForEndOfFrame();
        }
    }
}
