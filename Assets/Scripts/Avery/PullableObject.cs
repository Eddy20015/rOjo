using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullableObject : MonoBehaviour
{
    public float pullForce = 10f;

    private bool isBeingPulled = false;
    private Rigidbody2D playerRigidbody;
    private Player2DMovement playerMovement;

    void Start()
    {
        playerRigidbody = GameObject.FindGameObjectWithTag("Dancer").GetComponent<Rigidbody2D>();
        playerMovement = GameObject.FindGameObjectWithTag("Dancer").GetComponent<Player2DMovement>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isBeingPulled = true;
        }
        else
        {
            isBeingPulled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isBeingPulled && other.gameObject.CompareTag("Dancer"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(playerMovement.GetXTrans(), 0);
        }
    }
}