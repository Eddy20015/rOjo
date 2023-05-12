using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> objects;

    private void Start()
    {
        foreach (GameObject obj in objects)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject obj in objects)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1f;
        }
    }
}
