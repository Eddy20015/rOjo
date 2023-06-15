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
    private bool wasPaused;
    private bool donePlaying;

    protected override void Awake()
    {
        base.Awake();
        rawImage = GetComponent<RawImage>();
        rawImage.enabled = false;
        HandVideo.enabled = false;
    }

    private void Update()
    {
        if (GameStateManager.GetGameState() == GameStateManager.GAMESTATE.PAUSED)
            PauseForeground();
        else if (wasPaused && !donePlaying)
        {
            wasPaused = false;
            StartVPlayer();
        }
    }

    //This function assumes that all clips that will be played in level are
    //populated in Clips in the correct order with no repeats
    public void PlayNewForeground()
    {
        donePlaying = false;
        wasPaused = false;
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
        if (HandIdx - 1 >= 0 && HandIdx - 1 < HandClips.Length && HandIdx - 1 < HandVideoPositions.Length)
        {
            HandPlayer.clip = HandClips[HandIdx - 1];
            HandVideo.gameObject.transform.localPosition = new Vector3(HandVideoPositions[HandIdx - 1].x, HandVideoPositions[HandIdx - 1].y, 0);
        }
        HandPlayer.Play();
        HandVideo.enabled = true;
    }

    public void PauseForeground()
    {
        wasPaused = true;
        PauseVPlayer();
    }

    public void StopForeground()
    {
        StopVPlayer();
        rawImage.enabled = false;
        HandVideo.enabled = false;
    }

    private void DonePlaying(VideoPlayer vp)
    {
        donePlaying = true;
        VPlayer.Stop();
    }

    private void OnEnable()
    {
        VPlayer.loopPointReached += DonePlaying;
    }

    private void OnDestroy()
    {
        VPlayer.loopPointReached -= DonePlaying;
    }
}
