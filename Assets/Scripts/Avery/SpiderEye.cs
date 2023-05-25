using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SpiderEye : MonoBehaviour
{
    [SerializeField] Spider spider;

    // Update is called once per frame
    void Update()
    {
        return;

        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, transform.right, 10, ~LayerMask.GetMask("Default"));

        if (raycastHit2D)
        {
            if (raycastHit2D.collider.CompareTag("Dancer"))
            {
                spider.SetMoving(false);

            } else
            {
                spider.SetMoving(true);
            }
        } else
        {
            spider.SetMoving(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dancer"))
        {
            spider.SetMoving(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Dancer"))
        {
            spider.SetMoving(true);
        }
    }
}
