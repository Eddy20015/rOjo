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

    [SerializeField] int PlaySpeed;
    [SerializeField] int HighSpeed;
    [SerializeField] Transform pfEyeFieldOfView;
    [SerializeField] private float fov = 45f;
    [SerializeField] private float viewDistance = 3f; 
    private EyeFieldOfView eyeFieldOfView;
    private bool isActivated = false;
    private bool transitionComplete = false;
    private float eyeAngle;

    // Start is called before the first frame update
    protected new void Start()
    {

        eyeFieldOfView = Instantiate(pfEyeFieldOfView, null).GetComponent<EyeFieldOfView>();
        eyeFieldOfView.SetFOV(fov);
        eyeFieldOfView.SetViewDistance(viewDistance);
        eyeAngle = 90.0f;
        Player = GameObject.FindWithTag("Player");

        print("In the new start");
        base.Start();

        VPlayer.clip = InactiveClip;
        VPlayer.loopPointReached += CheckOver;

        StartVPlayer();
    }

    private void FixedUpdate()
    {
        eyeFieldOfView.SetOrigin(transform.position);
        
        if(Vector3.Distance(transform.position, Player.transform.position) < viewDistance) //If the player is within range of enemy.
        {
            Vector3 dirToPlayer = (Player.transform.position - transform.position).normalized;
            if (Mathf.Abs(Vector3.Angle(EyeFieldOfView.GetVectorFromAngle(eyeAngle), dirToPlayer)) < fov/2) //If the player is within the FOV.
            {
                Debug.Log("Inside FOV!");
                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, dirToPlayer, viewDistance); //If there aren't any obstacles between player and Enemy.
                if (raycastHit2D.collider != null) 
                {
                    if(raycastHit2D.collider.gameObject.tag == "Player")
                    {
                        // if(!isActivated)
                        // {
                        //     transitionComplete = false;
                        //     StartCoroutine(Activate());
                        // }
                        
                        // if(transitionComplete)
                        // {
                        Active();
                        // }
                    }
                    else
                    {
                        // if(isActivated)
                        // {
                        //     transitionComplete = false;
                        //     StartCoroutine(DeActivate());
                        // }

                        // if(transitionComplete)
                        // {
                        Inactive();
                        // }
                        //hit somethin else?
                    }
                }
                else
                {
                    Debug.Log("Raycast.collider == null");
                    Inactive();
                }
                
            }
            else
            {
                Debug.Log("Out of FOV");
                Inactive();
            }
        }
        else
        {
            Debug.Log("out of viewDistance!");
            Inactive();
        }
        
    }

    private void Inactive() //When the eye doesn't see you.
    {
        // if(isActivated) //When the eye loses track of you for the first time. Used to make transitions work.
        // {
        //     StartCoroutine(Deactivate());
        //     isActivated = false;
        // }

        VPlayer.clip = InactiveClip;

        int currentFrame = (int)VPlayer.frame; //Gets the current frame of the "eye"
        int totalFrames = (int)VPlayer.frameCount; //Gets the total amt of frames of the "eye" video.
        float framePct2Angle = (float)currentFrame/totalFrames; //Gets the percent of the current frame out of the whole video.
        eyeAngle = (framePct2Angle * 360f); 
        eyeFieldOfView.SetAimDirection(eyeAngle); // We multiply by -1 because the "eye" is currently going clockwise.
        
    }

    

    private IEnumerator Activate()
    {
            Debug.Log("Eye Activated!");

            isActivated = true;
            transitionComplete = false;

            //Speed up and finish the video forwards
            ClipSpeed(HighSpeed);


            //Do nothing until the active clip loop point is reached
            while (!bContinue) yield return new WaitForSeconds(1);
            bContinue = false;

            //Set the playback speed back to normal, but reverse it
            VPlayer.clip = TransitionClip;
            ClipSpeed(PlaySpeed);
            StartVPlayer();

            //Do nothing until the transition clip loop point is reached
            while (!bContinue) yield return new WaitForSeconds(1);
            bContinue = false;

            //Set the playback speed back to forward normal and let the Active clip play
            VPlayer.clip = ActiveClip;
            StartVPlayer();
            transitionComplete = true;
            
        
    }

    private void Active() //When the eye sees you.
    {
        VPlayer.clip = ActiveClip;
        
        Vector3 dirToPlayer = (Player.transform.position - transform.position);
        eyeAngle = EyeFieldOfView.GetAngleFromVectorFloat(dirToPlayer);
        eyeFieldOfView.SetAimDirection(dirToPlayer);
        float anglePct2Frame = Mathf.Abs(eyeAngle)/360f;
        var frame = VPlayer.frameCount * anglePct2Frame;
        VPlayer.frame = (long)frame;
        

    }

    private IEnumerator Deactivate()
    {
        Debug.Log("Eye Deactivated!");

        isActivated = false;
        //Speed up Active clip to completion
        VPlayer.loopPointReached += CheckOver;

        //Speed up and finish the video forwards
        ClipSpeed(HighSpeed);

        //Do nothing until the active clip loop point is reached
        while (!bContinue) yield return new WaitForSeconds(1);
        bContinue = false;

        //Set the playback speed back to normal, but reverse it
        VPlayer.clip = ReverseTransitionClip;
        ClipSpeed(PlaySpeed);

        //Do nothing until the transition clip loop point is reached
        while (!bContinue) yield return new WaitForSeconds(1);
        bContinue = false;

        //Set the playback speed back to forward normal and let the inactive clip play
        VPlayer.clip = InactiveClip;
        ClipSpeed(PlaySpeed);
    }
}
