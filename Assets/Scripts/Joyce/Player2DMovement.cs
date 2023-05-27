using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 11.5f;
    [SerializeField] private float jumpBoundary = .1f;
    private float xtrans;
    private bool isMovingObject = false;

    [Header("Physics")]
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerLayer;
    private int groundLayerInt, playerLayerInt;
    [Min(1)]
    [SerializeField] private float upGravity = 3f;
    [Min(0)]
    [SerializeField] private float downGravity = 2.2f;
    [Min(0)]
    [SerializeField] private float groundedGravity = 50f; // used to keep player on slope
    [SerializeField] private SlopeDetect slopeDetect;

    [Header("Graphics")]
    [SerializeField] private Animator anim;
    private Vector3 originalScale; // faces right

    [Header("Audio")]
    //[SerializeField] private string jumpSFX = "Jump";
    //[SerializeField] private string walkSFX = "Walk";
    [SerializeField] private AK.Wwise.Event jumpLanding;
    /*[SerializeField] private AK.Wwise.Event step;*/
    [SerializeField] private float stepFrequencey;
    private const float airTimeLimit = 0.5f;
    private float airTime;
    private bool isJumping;
    private float walkTime;

    void Start()
    {
        originalScale = transform.localScale;
        myRigidbody = GetComponent<Rigidbody2D>();

        isJumping = false;
        airTime = 0;
        walkTime = stepFrequencey;

        groundLayerInt = Mathf.RoundToInt(Mathf.Log(groundLayer.value, 2));
        playerLayerInt = Mathf.RoundToInt(Mathf.Log(playerLayer.value, 2));
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
                anim.SetBool("Moving", false);
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
                anim.SetBool("Moving", false);
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
            slopeDetect.enabled = false;
            anim.SetBool("Jumping", true);
            anim.SetFloat("JumpState", 0f); //initial jump animation
            //isJumping = true;

            //AudioManager.instance.Stop(jumpSFX);
            //AudioManager.instance.PlayOneShot(jumpSFX);
        }
        if (isJumping && Mathf.Abs(myRigidbody.velocity.y) <= jumpBoundary)
        {
            anim.SetFloat("JumpState", 1f); //landing animation
            anim.SetBool("Jumping", false);
            if (airTime > airTimeLimit)
            {
                print("landed");
                jumpLanding.Post(gameObject);
                slopeDetect.enabled = true;
                //isJumping = false;
            }
        }

        if (isJumping && Mathf.Abs(myRigidbody.velocity.y) > jumpBoundary && airTime > .2f)
        {
            anim.SetBool("Jumping", true);
            anim.SetFloat("JumpState", Mathf.Lerp(anim.GetFloat("JumpState"), 0.5f, Time.deltaTime * 5f)); //midair animation
        }

        isJumping = Mathf.Abs(myRigidbody.velocity.y) > jumpBoundary;

        if (isJumping) {
            myRigidbody.gravityScale = myRigidbody.velocity.y < 0 ? downGravity : upGravity;
            airTime += Time.deltaTime;

        } else {
            myRigidbody.gravityScale = slopeDetect.onSlope ? groundedGravity : upGravity;
            //myRigidbody.gravityScale = groundedGravity;
            airTime = 0;
        }

        //anim.SetFloat("Speed", xtrans);
        //anim.SetBool("FaceRight", faceRight);
        //anim.SetBool("Jump", isJumping);
    }

    private void FixedUpdate()
    {
        /*if (!isJumping && xtrans != 0 && Mathf.Abs(myRigidbody.velocity.y) <= jumpBoundary)
        {
            if (walkTime % stepFrequencey == 0) {
                step.Post(gameObject);
                //audiomanager.instance.stop(walksfx);
                //audiomanager.instance.playoneshot(walksfx);
            }
            walkTime++;
        }*/
        transform.Translate(xtrans * Time.fixedDeltaTime, 0, 0);
        Physics2D.IgnoreLayerCollision(groundLayerInt, playerLayerInt, (myRigidbody.velocity.y > jumpBoundary));
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