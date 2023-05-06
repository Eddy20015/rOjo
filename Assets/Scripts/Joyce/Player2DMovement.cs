using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float jumpBoundary = .1f;
    private float xtrans;

    [Header("Physics")]
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private int groundlayer;
    [SerializeField] private int playerLayer;
    [Min(1)]
    [SerializeField] private float upGravity;
    [Min(0)]
    [SerializeField] private float downGravity;

    [Header("Graphics")]
    [SerializeField] private Animator anim;
    private Vector3 originalScale; // faces right

    //[Header("Audio")]
    //[SerializeField] private string jumpSFX = "Jump";
    //[SerializeField] private string walkSFX = "Walk";
    //[SerializeField] private float stepFrequencey;
    private bool isJumping;
    private float walkTime;

    void Start()
    {
        originalScale = transform.localScale;
        myRigidbody = GetComponent<Rigidbody2D>();

        isJumping = false;
        //walkTime = stepFrequencey;
    }

    void Update()
    {
        xtrans = Input.GetAxis("Horizontal") * speed;
        if (xtrans > 0) // determines which way the player is facing
        {
            FlipRight();

            //anim.SetBool("Moving", true);

        }
        else if (xtrans < 0)
        {
            FlipLeft();

            //anim.SetBool("Moving", true);
        }
        else
        {
            //anim.SetBool("Moving", false);
            //AudioManager.instance.Stop(walkSFX);
            //walkTime = stepFrequencey;
        }


        if (Input.GetButtonDown("Jump") && Mathf.Abs(myRigidbody.velocity.y) <= jumpBoundary) // only allows jumping if not already up
        {
            myRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;

            //AudioManager.instance.Stop(jumpSFX);
            //AudioManager.instance.PlayOneShot(jumpSFX);
        }
        else if (Mathf.Abs(myRigidbody.velocity.y) <= jumpBoundary)
        {
            isJumping = false;
        }

        myRigidbody.gravityScale = myRigidbody.velocity.y < 0 ? downGravity: upGravity;

        //anim.SetFloat("Speed", xtrans);
        //anim.SetBool("FaceRight", faceRight);
        //anim.SetBool("Jump", isJumping);
    }

    private void FixedUpdate()
    {
        if (!isJumping && xtrans != 0)
        {
            //if (walkTime % stepFrequencey == 0)
            //{
            //    AudioManager.instance.Stop(walkSFX);
            //    AudioManager.instance.PlayOneShot(walkSFX);
            //}
            //walkTime ++;
        }
        transform.Translate(xtrans * Time.fixedDeltaTime, 0, 0);
        Physics2D.IgnoreLayerCollision(groundlayer, playerLayer, (myRigidbody.velocity.y > jumpBoundary));
    }
    private void FlipRight()
    {
        transform.localScale = originalScale;
    }
    
    private void FlipLeft()
    {
        Vector3 newScale = originalScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    public void ReverseXAxis()
    {
        speed = -speed;
    }

    public void GoTo(Vector3 goToTarget)
    {
        transform.position = goToTarget;
    }

}