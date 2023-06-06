using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{ 
    private KeyCode boundKey = KeyCode.E;
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
        if (isGrabbingObject && other.gameObject.name == "Facing Forward Check")
        {
            print("PLAYER FACING BOX!!");

            transform.Translate(playerMovement.GetXTrans() * Time.deltaTime, 0, 0);
;
            playerMovement.SetIsMovingObject(true);
        }
        else if (!isGrabbingObject && other.gameObject.CompareTag("Facing Forward Check"))
        {
            playerMovement.SetIsMovingObject(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Facing Forward Check")
        {
            playerMovement.SetIsMovingObject(false);
        }
    }
}