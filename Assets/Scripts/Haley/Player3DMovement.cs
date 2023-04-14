using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DMovement : MonoBehaviour
{
    [Tooltip("On: Control forward and backward movement\nOff: Continuously move forward")]
    [SerializeField] private bool controlMovement;
    [Tooltip("ONLY CHANGE IF PLAYER SCALE HAS CHANGED")]
    [SerializeField] private float playerHeight = 2f;

    [Header("Ground Movement")]
    [Tooltip("Affects speed for all motion")]
    [SerializeField] private float groundSpeed;
    [Tooltip("Affects speed of forward motion, but not side to side, default is 1")]
    [SerializeField] private float Forwardspeed = 1f;
    [Tooltip("Slows ground motion")]
    [SerializeField] private float groundDrag = 6f;

    [Header("Air Movement")]
    [SerializeField] private float airSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float airDrag = 2f;
    

    private float horizontalMovement;
    private Vector3 moveDirection;

    private bool isGrounded;

    Rigidbody rb;
    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);
        anim.SetBool("isGrounded", isGrounded);

        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if(controlMovement)
            moveDirection = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * horizontalMovement;
        else
            moveDirection = transform.forward * Forwardspeed + transform.right * horizontalMovement;

        if(Input.GetButton("Jump") && isGrounded) {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        rb.drag = isGrounded ? groundDrag : airDrag;

    }

    private void FixedUpdate()
    {
        if(isGrounded)
            rb.AddForce(moveDirection.normalized * groundSpeed, ForceMode.Acceleration);
        else
            rb.AddForce(moveDirection.normalized * airSpeed, ForceMode.Acceleration);
    }
}
