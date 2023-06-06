using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Chaser3D : EndingUI
{
    [Header("General Control Variables")]
    [SerializeField] private GameObject player;
    [SerializeField] private float speedOffset;
    private Player3DMovement pMove;

    private Vector3 moveDirection;

    private Rigidbody rb;
    private float speed;
    private bool moving = true;
    private float drag;

    public AK.Wwise.Event ChaseMusic;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezePositionY;

        pMove = player.GetComponent<Player3DMovement>();
        speed = pMove.getSpeed();
        drag = pMove.getDrag();
        moving = true;
    }

    private void OnEnable()
    {
        ChaseMusic.Post(gameObject);
    }

    void Update()
    {
        moveDirection = pMove.getMoveDirection();
        //moveDirection = transform.forward;
        rb.drag = drag;

        if(moving)
        {
            DistanceFromPlayer();
        }
    }

    private void FixedUpdate()
    {
        if(moving)
            rb.AddForce(moveDirection.normalized * (speed - speedOffset), ForceMode.Acceleration);
    }

    // Getting cause by chaser, CHANGE AS NEEDED
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")) {
            Halt();
            pMove.enabled = false;
            // play hand vid here instead, then play menu
            PlayMenu();
        }

        /*print("trigger on " + other.name);
        Player3DMovement p = other.GetComponentInParent<Player3DMovement>();
        if(p != null) {
            p.enabled = false; // Stop them from moving
            text.SetActive(true);
            gameObject.SetActive(false);
        }*/
    }

    public void Halt()
    {
        moving = false;
    }

    public void DistanceFromPlayer()
    {
        float currDistance = Mathf.Abs(transform.position.z - player.transform.position.z);

        AkSoundEngine.SetRTPCValue("MonsterDistance", currDistance);
    }
}
