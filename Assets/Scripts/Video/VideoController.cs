using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public abstract class VideoController : MonoBehaviour
{
    protected VideoPlayer VPlayer;

    protected bool bContinue;

    protected virtual void Awake()
    {
        print("In the old awake");
        VPlayer = gameObject.GetComponent<VideoPlayer>();
    }

    protected void StartVPlayer()
    {
        VPlayer.Play();
    }

    protected void PauseVPlayer()
    {
        VPlayer.Pause();
    }

    protected void StopVPlayer()
    {
        VPlayer.Stop();
    }

    protected void ClipSpeed(int Speed)
    {
        VPlayer.playbackSpeed = Speed;
        StartVPlayer();
    }

    protected void CheckOver(VideoPlayer vp)
    {
        bContinue = true;
    }
}
