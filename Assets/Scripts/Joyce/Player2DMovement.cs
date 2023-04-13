using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 20f;
    private float xtrans;

    [Header("Physics")]
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private int groundlayer;
    [SerializeField] private int playerLayer;

    [Header("Graphics")]
    [SerializeField] private Animator anim;
    private bool faceRight;
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

        faceRight = true;
        isJumping = false;
        //walkTime = stepFrequencey;
    }

    void Update()
    {
        xtrans = Input.GetAxisRaw("Horizontal") * speed;
        if (xtrans > 0) // determines which way the player is facing
        {
            faceRight = true;
            FlipRight();

            //anim.SetBool("Moving", true);

        }
        else if (xtrans < 0)
        {
            faceRight = false;
            FlipLeft();

            //anim.SetBool("Moving", true);
        }
        else
        {
            //anim.SetBool("Moving", false);
            //AudioManager.instance.Stop(walkSFX);
            //walkTime = stepFrequencey;
        }


        if (Input.GetButtonDown("Jump") && Mathf.Abs(myRigidbody.velocity.y) < 0.1f) // only allows jumping if not already up
        {
            myRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;

            //AudioManager.instance.Stop(jumpSFX);
            //AudioManager.instance.PlayOneShot(jumpSFX);
        }
        else if (Mathf.Abs(myRigidbody.velocity.y) < 0.1f)
        {
            isJumping = false;
        }

        Physics2D.IgnoreLayerCollision(groundlayer, playerLayer, (myRigidbody.velocity.y > 0.1f));

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
            walkTime ++;
        }
        transform.Translate(xtrans * Time.fixedDeltaTime, 0, 0);
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