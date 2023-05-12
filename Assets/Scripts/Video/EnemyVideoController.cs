using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EnemyVideoController : VideoController
{
    [SerializeField] GameObject Player;
    [SerializeField] VideoClip InactiveClip;
    [SerializeField] VideoClip ActiveClip;
    [SerializeField] VideoClip TransitionClip;
    [SerializeField] VideoClip ReverseTransitionClip;

    [SerializeField] int PlaySpeed = 1;
    [SerializeField] int HighSpeed = 10;
    [SerializeField] Transform pfEyeFieldOfView;
    [SerializeField] private float fov = 45f;
    [SerializeField] private float viewDistance = 3f; 
    [SerializeField] private float lookingDuration = .01f;
    private EyeFieldOfView eyeFieldOfView;
    private bool isActivated = false;
    private bool isLooking = false;
    
    
    private float elapsedTime;
    private float eyeFrameFoundPct;
    private float eyeAngle;

    private enum State
    {
        Active,
        Inactive,
        Active2Inactive,
        Inactive2Active,
        Looking,
    }

    private State state;

    // Start is called before the first frame update
    protected new void Start()
    {

        eyeFieldOfView = Instantiate(pfEyeFieldOfView, null).GetComponent<EyeFieldOfView>();
        eyeFieldOfView.SetFOV(fov);
        eyeFieldOfView.SetViewDistance(viewDistance);
        eyeAngle = 90.0f;
        Player = GameObject.FindWithTag("Dancer");

        print("In the new start");
        base.Start();

        state = State.Inactive;

        VPlayer.clip = InactiveClip;
        VPlayer.loopPointReached += CheckOver;

        StartVPlayer();
    }

    private void FixedUpdate()
    {
        eyeFieldOfView.SetOrigin(transform.position);


        switch(state)
        {
            default:
            case State.Inactive:
                Inactive();
                FindDancer();
                break;
            case State.Inactive2Active:
                break;
            case State.Active:
                FindDancer();
                break;
            case State.Looking:
                FindDancer();
                break;
            case State.Active2Inactive:
                FindDancer();
                break;

        }
    }


    private void FindDancer()
    {
        if(Vector3.Distance(transform.position, Player.transform.position) < viewDistance) //If the player is within range of enemy.
        {
            Vector3 dirToPlayer = (Player.transform.position - transform.position).normalized;
            if (Mathf.Abs(Vector3.Angle(EyeFieldOfView.GetVectorFromAngle(eyeAngle), dirToPlayer)) < fov/2) //If the player is within the FOV.
            {
                Debug.Log("Inside FOV!");
                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, dirToPlayer, viewDistance); //If there aren't any obstacles between player and Enemy.
                if (raycastHit2D.collider != null) 
                {
                    if(raycastHit2D.collider.gameObject.tag == "Dancer")
                    {
                        if (state == State.Inactive)
                        {
                            float angleToPlayer = EyeFieldOfView.GetAngleFromVectorFloat(dirToPlayer);
                            eyeFrameFoundPct = Mathf.Abs(eyeAngle)/360f;
                            StartCoroutine(Activate());
                        }

                        if (state == State.Active) Active();

                        if (state == State.Looking || state == State.Active2Inactive)
                        {
                            StopAllCoroutines();
                            state = State.Active;
                        }
                        
                    }
                    else
                    {
                        if (state == State.Active) StartCoroutine(Looking());
                    }
                }
                else
                {
                    if (state == State.Active) StartCoroutine(Looking());
                }
                
            }
            else
            {
                if (state == State.Active) StartCoroutine(Looking());
            }
        }
        else
        {
            if (state == State.Active) StartCoroutine(Looking());
        }
        
    }

    private void Inactive() //When the eye doesn't see you.
    {
        // if(isActivated) //When the eye loses track of you for the first time. Used to make transitions work.
        // {
        //     StartCoroutine(Deactivate());
        //     isActivated = false;
        // }

        int currentFrame = (int)VPlayer.frame; //Gets the current frame of the "eye"
        int totalFrames = (int)VPlayer.frameCount; //Gets the total amt of frames of the "eye" video.
        float framePct2Angle = (float)currentFrame/totalFrames; //Gets the percent of the current frame out of the whole video.
        eyeAngle = (framePct2Angle * 360f); 
        eyeFieldOfView.SetAimDirection(eyeAngle); 
    }

   

    

    private IEnumerator Activate()
    {
            Debug.Log("Eye Activated!");

            state = State.Inactive2Active; //Tells the state machine to wait till this is done.

            //Speed up and finish the video forwards

            //Set the playback speed back to normal, but reverse it
            VPlayer.clip = TransitionClip;

            //Do nothing until the transition clip loop point is reached
            while (!bContinue) 
            {
                yield return new WaitForSeconds(.01f);
            }
            bContinue = false;

            VPlayer.clip = ActiveClip;
            var frame = VPlayer.frameCount * eyeFrameFoundPct;
            VPlayer.frame = (long)frame + 6;
            ClipSpeed(HighSpeed);
            //Do nothing until the active clip loop point is reached
            // while (!bContinue) 
            // {
            //     yield return new WaitForSeconds(.01f);
            // }
            // bContinue = false;
            while (Mathf.Abs((int)VPlayer.frame - frame) > 5)
            {
                Debug.Log(VPlayer.frame);
                yield return new WaitForSeconds(.001f);
            }
            bContinue = false;
            PauseVPlayer();
            //Set the playback speed back to forward normal and let the Active clip play
            
            // ClipSpeed(PlaySpeed);

            Debug.Log("Back to spotted frame");

            state = State.Active;
            
            
            
        
    }

    private void Active() //When the eye sees you.
    {   
        PauseVPlayer();
        Vector3 dirToPlayer = (Player.transform.position - transform.position);
        eyeAngle = EyeFieldOfView.GetAngleFromVectorFloat(dirToPlayer);
        eyeFieldOfView.SetAimDirection(dirToPlayer);
        float anglePct2Frame = Mathf.Abs(eyeAngle)/360f;
        var frame = VPlayer.frameCount * anglePct2Frame;
        VPlayer.frame = (long)frame;
        

    }

    private IEnumerator Looking() //When the eye looks around after it loses you.
    {
        
        float initEyeAngle = eyeAngle;
        float lookRight = 30;
        float lookLeft = -30;

        elapsedTime = 0;
        state = State.Looking;

        while(elapsedTime < lookingDuration)
        {
            smoothLook(initEyeAngle, initEyeAngle + lookRight);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(0.2f);
        elapsedTime = 0;
        initEyeAngle = eyeAngle;
        while(elapsedTime < lookingDuration)
        {
            smoothLook(initEyeAngle, initEyeAngle + lookLeft*2);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(0.2f);
        elapsedTime = 0;
        initEyeAngle = eyeAngle;
        while(elapsedTime < lookingDuration)
        {
            smoothLook(initEyeAngle, initEyeAngle + lookRight);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        StartCoroutine(Deactivate());
    }

    private void smoothLook(float lookStart, float lookDest)
    {
        float percentageComplete = elapsedTime / lookingDuration;
        eyeAngle = Mathf.Lerp(lookStart, lookDest, percentageComplete);
        eyeFieldOfView.SetAimDirection(eyeAngle);
        float anglePct2Frame = Mathf.Abs(eyeAngle%360f)/360f;
        var frame = VPlayer.frameCount * anglePct2Frame;
        VPlayer.frame = (long)frame;
        elapsedTime += Time.deltaTime;
    }

        

    private IEnumerator Deactivate()
    {
        Debug.Log("Eye Deactivated!");

        state = State.Active2Inactive; //Tells the state machine to wait till this is done.
        //Speed up Active clip to completion

        //Speed up and finish the video forwards
        StartVPlayer();
        
        //Do nothing until the active clip loop point is reached
        while (!bContinue)
        {
            Inactive();
            yield return new WaitForSeconds(.01f);
        } 
        bContinue = false;

        //Set the playback speed back to normal, but reverse it
        VPlayer.clip = ReverseTransitionClip;
        ClipSpeed(PlaySpeed);

        //Do nothing until the transition clip loop point is reached
        while (!bContinue)
        {
            yield return new WaitForSeconds(.01f);
        } 
        bContinue = false;

        //Set the playback speed back to forward normal and let the inactive clip play
        VPlayer.clip = InactiveClip;
        ClipSpeed(PlaySpeed);

        state = State.Inactive;
        
    }
}
