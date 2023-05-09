using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser3D : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speedOffset;

    // FOR TESTING, CHANGE AS NEEDED
    [SerializeField] private GameObject text;
    private Player3DMovement pMove;

    private Vector3 moveDirection;

    private Rigidbody rb;
    private float speed;
    private float drag;

    private void Awake()
    {
        text.SetActive(false);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezePositionY;

        pMove = player.GetComponent<Player3DMovement>();
        speed = pMove.getSpeed();
        drag = pMove.getDrag();
    }

    void Update()
    {
        moveDirection = pMove.getMoveDirection();
        //moveDirection = transform.forward;
        rb.drag = drag;
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection.normalized * (speed - speedOffset), ForceMode.Acceleration);
    }

    // Getting cause by chaser, CHANGE AS NEEDED
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")) {
            GameStateManager.Pause();
            text.SetActive(true);
        }

        /*print("trigger on " + other.name);
        Player3DMovement p = other.GetComponentInParent<Player3DMovement>();
        if(p != null) {
            p.enabled = false; // Stop them from moving
            text.SetActive(true);
            gameObject.SetActive(false);
        }*/
    }
}
