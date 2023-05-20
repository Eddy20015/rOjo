using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundVideoTrigger : MonoBehaviour
{
    private ForegroundController foregroundController;

    private void Start()
    {
        foregroundController = FindObjectOfType<ForegroundController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dancer")
        {
            foregroundController.StopForeground();
            foregroundController.PlayNewForeground();
            GetComponent<Collider2D>().enabled = false;
        }
    }
}