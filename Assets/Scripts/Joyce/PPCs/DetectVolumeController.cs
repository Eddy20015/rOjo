using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class DetectVolumeController : PostProcessController
{
    [Header("Exposure Reference")]
    [SerializeField] private Image exposure;

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

    private void FixedUpdate()
    {
        vignetteS.intensity.Override(Mathf.Lerp(vignIntenMin, vignIntenMax, exposure.fillAmount));
        
        lensDistortS.intensity.Override(Mathf.Lerp(lensIntenMin, lensIntenMin, exposure.fillAmount));
        lensDistortS.yMultiplier.Override(Mathf.Lerp(lensYMultMin, lensYMultMax, exposure.fillAmount));
        
        paniniS.distance.Override(Mathf.Lerp(paniniDistMin, paniniDistMax, exposure.fillAmount));
        paniniS.cropToFit.Override(Mathf.Lerp(paniniCropMin, paniniCropMax, exposure.fillAmount));

        filmGrainS.intensity.Override(Mathf.Lerp(filmIntenMin, filmIntenMax, exposure.fillAmount));

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
}
