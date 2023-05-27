using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ForegroundController : VideoController
{
    [Tooltip("Fill this with all clips in order")]
    [SerializeField] private VideoClip[] Clips;
    private RawImage rawImage;

    int CurrIdx = 0;

    protected override void Start()
    {
        base.Start();
        rawImage = GetComponent<RawImage>();
        rawImage.enabled = false;
    }

    //This function assumes that all clips that will be played in level are
    //populated in Clips in the correct order with no repeats
    public void PlayNewForeground()
    {
        if(CurrIdx >= 0)
        {
            VPlayer.clip = Clips[CurrIdx % Clips.Length];
            CurrIdx++;
        }
        StartVPlayer();
        rawImage.enabled = true;
    }

    public void PauseForeground()
    {
        PauseVPlayer();
    }

    public void StopForeground()
    {
        StopVPlayer();
        rawImage.enabled = false;
    }
}
