using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class DetectVolumeController : PostProcessController
{
    [Header("Exposure Reference")]
    [SerializeField] private Image exposure;
    private bool refExposure = true;

    [Header("Fade Out Controls")]
    [SerializeField] private float fadeTime;
    private float timeElapsed;

    private Vignette vignetteS = null;
    [Header("Vignette Settings")]
    [SerializeField] private float vignIntenMin;
    [SerializeField] private float vignIntenMax;

    private LensDistortion lensDistortS = null;
    [Header("Lens Distortion Settings")]
    [SerializeField] private float lensIntenMin;
    [SerializeField] private float lensIntenMax;
    [SerializeField] private float lensYMultMin;
    [SerializeField] private float lensYMultMax;

    private PaniniProjection paniniS = null;
    [Header("Panini Projection Settings")]
    [SerializeField] private float paniniDistMin;
    [SerializeField] private float paniniDistMax;
    [SerializeField] private float paniniCropMin;
    [SerializeField] private float paniniCropMax;

    private FilmGrain filmGrainS = null;
    [Header("Film Grain Settings")]
    [SerializeField] private float filmIntenMin;
    [SerializeField] private float filmIntenMax;

    private void Awake()
    {
        refExposure = true;
        timeElapsed = 0;

        VignetteSetUp();
        LensDistortionSetUp();
        PaniniSetUp();
        FilmGrainSetUp();
    }

    private void VignetteSetUp()
    {
        vProfile.TryGet(out vignetteS);
        vignIntenMax = (float)vignetteS.intensity;
        vignetteS.intensity.Override(vignIntenMin);
    }

    private void LensDistortionSetUp()
    {
        vProfile.TryGet(out lensDistortS);
        lensIntenMax = (float)lensDistortS.intensity;
        lensDistortS.intensity.Override(lensIntenMin);

        lensYMultMax = (float)lensDistortS.yMultiplier;
        lensDistortS.yMultiplier.Override(lensYMultMin);
    }

    private void PaniniSetUp()
    {
        vProfile.TryGet(out paniniS);
        paniniDistMax = (float)paniniS.distance;
        paniniS.distance.Override(paniniDistMin);

        paniniCropMax = (float)paniniS.cropToFit;
        paniniS.cropToFit.Override(paniniCropMin);
    }

    private void FilmGrainSetUp()
    {
        vProfile.TryGet(out filmGrainS);
        filmIntenMax = (float)filmGrainS.intensity;
        filmGrainS.intensity.Override(filmIntenMin);
    }

    private void Update()
    {
        if (refExposure)
        {
            LerpEffects(exposure.fillAmount);
        }
        else
        {
            if(timeElapsed < fadeTime)
            {
                LerpEffectsFade(timeElapsed / fadeTime);
                timeElapsed += Time.deltaTime;
            }
        }

    }

    private void LerpEffects(float lerpVal)
    {
        vignetteS.intensity.Override(Mathf.Lerp(vignIntenMin, vignIntenMax, lerpVal));

        lensDistortS.intensity.Override(Mathf.Lerp(lensIntenMin, lensIntenMin, lerpVal));
        lensDistortS.yMultiplier.Override(Mathf.Lerp(lensYMultMin, lensYMultMax, lerpVal));

        paniniS.distance.Override(Mathf.Lerp(paniniDistMin, paniniDistMax, lerpVal));
        paniniS.cropToFit.Override(Mathf.Lerp(paniniCropMin, paniniCropMax, lerpVal));

        filmGrainS.intensity.Override(Mathf.Lerp(filmIntenMin, filmIntenMax, lerpVal));
    }

    private void LerpEffectsFade(float lerpVal)
    {
        vignetteS.intensity.Override(Mathf.Lerp((float)vignetteS.intensity, vignIntenMin, lerpVal));

        lensDistortS.intensity.Override(Mathf.Lerp((float)lensDistortS.intensity, lensIntenMin, lerpVal));
        lensDistortS.yMultiplier.Override(Mathf.Lerp((float)lensDistortS.yMultiplier, lensYMultMin, lerpVal));

        paniniS.distance.Override(Mathf.Lerp((float)paniniS.distance, paniniDistMin, lerpVal));
        paniniS.cropToFit.Override(Mathf.Lerp((float)paniniS.cropToFit, paniniCropMin, lerpVal));

        filmGrainS.intensity.Override(Mathf.Lerp((float)filmGrainS.intensity, filmIntenMin, lerpVal));
    }

    private void OnDestroy()
    {
        vignetteS.intensity.Override(vignIntenMax);
        
        lensDistortS.intensity.Override(lensIntenMax);
        lensDistortS.yMultiplier.Override(lensYMultMax);
        
        paniniS.distance.Override(paniniDistMax);
        paniniS.cropToFit.Override(paniniCropMax);

        filmGrainS.intensity.Override(filmIntenMax);
    }

    public override void FadeOutEffects()
    {
        refExposure = false;
        timeElapsed = 0;
    }
}
