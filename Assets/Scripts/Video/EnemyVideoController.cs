using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EnemyVideoController : VideoController
{
    [SerializeField] VideoClip InactiveClip;
    [SerializeField] VideoClip ActiveClip;
    [SerializeField] VideoClip TransitionClip;
    [SerializeField] VideoClip ReverseTransitionClip;

    [SerializeField] int PlaySpeed;
    [SerializeField] int HighSpeed;

    // Start is called before the first frame update
    protected new void Start()
    {
        print("In the new start");
        base.Start();

        VPlayer.clip = InactiveClip;
        StartVPlayer();
    }

    public void Activate()
    {

    }

    public void Deactivate()
    {
        //Speed up Active clip to completion
        VPlayer.loopPointReached += CheckOver;

        //Speed up and finish the video forwards
        ClipSpeed(HighSpeed);

        //Do nothing until the active clip loop point is reached
        while (!bContinue) ;
        bContinue = false;

        //Set the playback speed back to normal, but reverse it
        VPlayer.clip = ReverseTransitionClip;
        ClipSpeed(PlaySpeed);

        //Do nothing until the transition clip loop point is reached
        while (!bContinue) ;
        bContinue = false;

        //Set the playback speed back to forward normal and let the inactive clip play
        VPlayer.clip = InactiveClip;
        ClipSpeed(PlaySpeed);
    }
}
