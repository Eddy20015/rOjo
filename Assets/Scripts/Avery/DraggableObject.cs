using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    [SerializeField] AK.Wwise.Event startPush;
    [SerializeField] AK.Wwise.Event stopPush;
    private bool playshingPushSound;

    private KeyCode boundKey = KeyCode.E;
    private bool isGrabbingObject = false;
    private Rigidbody2D playerRigidbody;
    private Player2DMovement playerMovement;

    void Start()
    {
        playerRigidbody = GameObject.FindGameObjectWithTag("Dancer").GetComponent<Rigidbody2D>();
        playerMovement = GameObject.FindGameObjectWithTag("Dancer").GetComponent<Player2DMovement>();
        playshingPushSound = false;
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
            stopPush.Post(gameObject);
            playshingPushSound = false;
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
            if (Mathf.Sign(gameObject.transform.position.x - other.gameObject.transform.position.x) == Mathf.Sign(other.gameObject.transform.parent.transform.localScale.x)) //Check scale if facing the right way from origin of statue.
            {

                //print("PLAYER FACING BOX!!");
                transform.Translate(playerMovement.GetXTrans() * Time.deltaTime, 0, 0);
                ;
                playerMovement.SetIsMovingObject(true);
                if (!playshingPushSound)
                {
                    startPush.Post(gameObject);
                    playshingPushSound = true;
                }
            }
        }
        else if (!isGrabbingObject && other.gameObject.name == "Facing Forward Check")
        {
            playerMovement.SetIsMovingObject(false);
            stopPush.Post(gameObject);
            playshingPushSound = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Facing Forward Check")
        {
            playerMovement.SetIsMovingObject(false);
            stopPush.Post(gameObject);
            playshingPushSound = false;
        }
    }
}