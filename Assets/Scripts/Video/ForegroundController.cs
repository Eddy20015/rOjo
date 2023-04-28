using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ForegroundController : VideoController
{
    [Tooltip("Fill this with all clips in order")]
    [SerializeField] VideoClip[] Clips;

    int CurrIdx = 0;


    //This function assumes that all clips that will be played in level are
    //populated in Clips in the correct order with no repeats
    public void PlayNewForeground()
    {
        if(CurrIdx >= 0 && CurrIdx < Clips.Length)
        {
            VPlayer.clip = Clips[CurrIdx++];
        }
        StartVPlayer();
    }

    public void PauseForeground()
    {
        PauseVPlayer();
    }

    public void StopForeground()
    {
        StopVPlayer();
    }
}
