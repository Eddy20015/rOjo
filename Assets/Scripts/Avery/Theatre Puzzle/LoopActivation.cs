using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopActivation : MonoBehaviour
{
    [SerializeField] GameObject loop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dancer"))
            loop.SetActive(true);
    }
}
