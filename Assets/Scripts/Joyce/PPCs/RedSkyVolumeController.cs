using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RedSkyVolumeController : PostProcessController
{
    [Header("Fade In Controls")]
    [SerializeField] private float fadeTime;
    private float timeElapsed = 0;

    private FilmGrain filmGrainS = null;
    [Header("Film Grain Settings")]
    [SerializeField] private float filmIntenMin;
    [SerializeField] private float filmIntenMax;

    private LensDistortion lensDistortS = null;
    [Header("Lens Distortion Settings")]
    [SerializeField] private float lensIntenMin;
    [SerializeField] private float lensIntenMax;
    [SerializeField] private float lensXMultMin;
    [SerializeField] private float lensXMultMax;
    [SerializeField] private float lensScaleMin;
    [SerializeField] private float lensScaleMax;

    private Vignette vignetteS = null;
    [Header("Vignette Settings")]
    [SerializeField] private float vignIntenMin;
    [SerializeField] private float vignIntenMax;

    private DepthOfField depthS = null;
    [Header("Depth of Field Settings")]
    [SerializeField] private float depthStartMin;
    [SerializeField] private float depthStartMax;

    private ColorAdjustments colorAdjustS;
    [Header("Color Adjustements Settings")]
    [SerializeField] private float colorAdjustPostMin;
    [SerializeField] private float colorAdjustPostMax;

    private void Awake()
    {
        timeElapsed = 0;
    }


    public override void FadeInEffects()
    {
        
    }
    private void OnDestroy()
    {

    }
}
