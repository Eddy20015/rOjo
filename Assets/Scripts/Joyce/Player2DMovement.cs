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
    private bool isMovingObject = false;

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

    [Header("Audio")]
    //[SerializeField] private string jumpSFX = "Jump";
    //[SerializeField] private string walkSFX = "Walk";
    [SerializeField] private AK.Wwise.Event jumpLanding;
    [SerializeField] private AK.Wwise.Event step;
    [SerializeField] private float stepFrequencey;
    private bool isJumping;
    private float walkTime;

    void Start()
    {
        originalScale = transform.localScale;
        myRigidbody = GetComponent<Rigidbody2D>();

        isJumping = false;
        walkTime = stepFrequencey;
    }

    void Update()
    {
        xtrans = Input.GetAxis("Horizontal") * speed;
        if (xtrans > 0) // determines which way the player is facing
        {
            if (!isMovingObject)
            {
                FlipRight();

                anim.SetBool("Moving", true);
            }
            else
            {
                xtrans /= 2;
                // set pushing/pulling animation here
            }
        }
        else if (xtrans < 0)
        {
            if (!isMovingObject)
            {
                FlipLeft();

                anim.SetBool("Moving", true);
            }
            else
            {
                xtrans /= 2;
                // set pushing/pulling animation here
            }
        }
        else
        {
            anim.SetBool("Moving", false);
            //AudioManager.instance.Stop(walkSFX);
            walkTime = stepFrequencey;
        }


        if (Input.GetButtonDown("Jump") && Mathf.Abs(myRigidbody.velocity.y) <= jumpBoundary) // only allows jumping if not already up
        {
            myRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            //isJumping = true;

            //AudioManager.instance.Stop(jumpSFX);
            //AudioManager.instance.PlayOneShot(jumpSFX);
        }
        if (isJumping && Mathf.Abs(myRigidbody.velocity.y) <= jumpBoundary)
        {
            print("landed");
            jumpLanding.Post(gameObject);
            //isJumping = false;
        }

        isJumping = Mathf.Abs(myRigidbody.velocity.y) > jumpBoundary;

        myRigidbody.gravityScale = myRigidbody.velocity.y < 0 ? downGravity: upGravity;

        //anim.SetFloat("Speed", xtrans);
        //anim.SetBool("FaceRight", faceRight);
        //anim.SetBool("Jump", isJumping);
    }

    private void FixedUpdate()
    {
        if (!isJumping && xtrans != 0 && Mathf.Abs(myRigidbody.velocity.y) <= jumpBoundary)
        {
            if (walkTime % stepFrequencey == 0) {
                step.Post(gameObject);
                //audiomanager.instance.stop(walksfx);
                //audiomanager.instance.playoneshot(walksfx);
            }
            walkTime++;
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

    public float GetXTrans()
    {
        return xtrans;
    }

    public void SetIsMovingObject(bool b)
    {
        isMovingObject = b;
    }
}