using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{ 
    private KeyCode boundKey = KeyCode.LeftShift;
    private bool isGrabbingObject = false;
    private Rigidbody2D playerRigidbody;
    private Player2DMovement playerMovement;

    void Start()
    {
        playerRigidbody = GameObject.FindGameObjectWithTag("Dancer").GetComponent<Rigidbody2D>();
        playerMovement = GameObject.FindGameObjectWithTag("Dancer").GetComponent<Player2DMovement>();
    }

    void Update()
    {
        if (Input.GetKey(boundKey))
        {
            isGrabbingObject = true;
        }
        else
        {
            isGrabbingObject = false;
            playerMovement.SetIsMovingObject(false);
        }

        if (Input.GetKeyUp(boundKey))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isGrabbingObject && other.gameObject.CompareTag("Dancer"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(playerMovement.GetXTrans(), GetComponent<Rigidbody2D>().velocity.y);
            playerMovement.SetIsMovingObject(true);
        }
        else if (!isGrabbingObject && other.gameObject.CompareTag("Dancer"))
        {
            playerMovement.SetIsMovingObject(false);
        }
    }
}