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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Activate();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            BeginDeactivate();
        }
    }

    public void Activate()
    {
        VPlayer.clip = ActiveClip;
        StartVPlayer();
    }

    public void BeginDeactivate()
    {
        VPlayer.loopPointReached += ReverseClip;

        //Speed up and finish the video forwards
        ClipSpeed(HighSpeed);
    }

    public void ReverseClip(VideoPlayer vp)
    {
        VPlayer.loopPointReached -= ReverseClip;
        VPlayer.loopPointReached += EndDeactivate;

        //Set the playback speed back to normal, and play ReverseTransitionClip
        VPlayer.clip = ReverseTransitionClip;
        ClipSpeed(PlaySpeed);
    }

    public void EndDeactivate(VideoPlayer vp)
    {
        VPlayer.loopPointReached -= EndDeactivate;

        //Set the playback speed back to forward normal and let the inactive clip play
        VPlayer.clip = InactiveClip;
        StartVPlayer();
    }
}
