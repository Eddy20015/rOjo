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

    [SerializeField] private RawImage HandVideo;
    [SerializeField] private VideoClip[] HandClips;
    [SerializeField] private Vector3[] HandVideoPositions;

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

    public void PlayNewHandVideo(int HandIdx)
    {
        VideoPlayer HandPlayer = HandVideo.gameObject.GetComponent<VideoPlayer>();
        if (HandIdx - 1 >= 0 && HandIdx < HandClips.Length && HandIdx < HandVideoPositions.Length)
        {
            HandPlayer.clip = HandClips[HandIdx - 1];
            HandVideo.gameObject.transform.position = HandVideoPositions[HandIdx - 1];
        }
        HandVideo.enabled = true;
        
        HandPlayer.Play();
    }

    public void PauseForeground()
    {
        PauseVPlayer();
    }

    public void StopForeground()
    {
        StopVPlayer();
        rawImage.enabled = false;
        HandVideo.enabled = false;
    }
}
