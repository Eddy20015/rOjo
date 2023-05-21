using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RedSkyVolumeController : PostProcessController
{
    [Header("Toggle Time")]
    [SerializeField] private float toggleTime = 1.6f;

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
    [SerializeField] private float colorAdjustConstrastMin;
    [SerializeField] private float colorAdjustConstrastMax;
    [SerializeField] private float colorAdjustSatMin;
    [SerializeField] private float colorAdjustSatMax;

    private ColorCurves colorCurveS;
    private MotionBlur motionS;

    private bool fadeIn;

    private void Awake()
    {
        timeElapsed = 0;
        FilmGrainSetUp();
        LensDistortionSetUp();
        VignetteSetUp();
        DepthOVSetUp();
        ColorAdjustSetUp();
        ColorCurvesSetUp();
        MotionBlurSetUp();

        fadeIn = false;
    }

    private void FilmGrainSetUp()
    {
        vProfile.TryGet(out filmGrainS);
        filmIntenMax = (float)filmGrainS.intensity;
        filmGrainS.intensity.Override(filmIntenMin);
    }

    private void LensDistortionSetUp()
    {
        vProfile.TryGet(out lensDistortS);
        lensIntenMax = (float)lensDistortS.intensity;
        lensDistortS.intensity.Override(lensIntenMin);

        lensXMultMax = (float)lensDistortS.xMultiplier;
        lensDistortS.xMultiplier.Override(lensXMultMin);

        lensScaleMax = (float)lensDistortS.scale;
        lensDistortS.scale.Override(lensScaleMin);
    }

    private void VignetteSetUp()
    {
        vProfile.TryGet(out vignetteS);
        vignIntenMax = (float)vignetteS.intensity;
        vignetteS.intensity.Override(vignIntenMin);
    }

    private void DepthOVSetUp()
    {
        vProfile.TryGet(out depthS);
        depthStartMax = (float)depthS.gaussianStart;
        depthS.gaussianStart.Override(depthStartMin);
    }

    private void ColorAdjustSetUp()
    {
        vProfile.TryGet(out colorAdjustS);
        colorAdjustPostMax = (float)colorAdjustS.postExposure;
        colorAdjustS.postExposure.Override(colorAdjustPostMin);

        colorAdjustConstrastMax = (float)colorAdjustS.contrast;
        colorAdjustS.contrast.Override(colorAdjustConstrastMin);

        colorAdjustSatMax = (float)colorAdjustS.saturation;
        colorAdjustS.saturation.Override(colorAdjustSatMin);
    }

    private void ColorCurvesSetUp()
    {
        vProfile.TryGet(out colorCurveS);
        colorCurveS.active = false;
    }
    
    private void MotionBlurSetUp()
    {
        vProfile.TryGet(out motionS);
        motionS.active = false;
    }

    private void Update()
    {
        if (fadeIn && timeElapsed < fadeTime)
        {
            LerpEffectsFade(timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;
        }
    }

    public override void ToggleEffect(bool on)
    {
        depthS.gaussianStart.Override(on ? depthStartMax : depthStartMin);
        colorCurveS.active = on;
    }

    private void LerpEffectsFade(float lerpVal)
    {
        filmGrainS.intensity.Override(Mathf.Lerp((float)filmGrainS.intensity, filmIntenMax, lerpVal));

        lensDistortS.intensity.Override(Mathf.Lerp((float)lensDistortS.intensity, lensIntenMax, lerpVal));
        lensDistortS.xMultiplier.Override(Mathf.Lerp((float)lensDistortS.xMultiplier, lensXMultMax, lerpVal));
        lensDistortS.scale.Override(Mathf.Lerp((float)lensDistortS.scale, lensScaleMax, lerpVal));

        vignetteS.intensity.Override(Mathf.Lerp((float)vignetteS.intensity, vignIntenMin, lerpVal));

        //depthS.gaussianStart.Override(Mathf.Lerp((float)depthS.gaussianStart, depthStartMax, lerpVal));

        colorAdjustS.postExposure.Override(Mathf.Lerp((float)colorAdjustS.postExposure, colorAdjustPostMax, lerpVal));
        colorAdjustS.contrast.Override(Mathf.Lerp((float)colorAdjustS.contrast, colorAdjustConstrastMax, lerpVal));
        colorAdjustS.saturation.Override(Mathf.Lerp((float)colorAdjustS.saturation, colorAdjustSatMax, lerpVal));
    }

    public override void FadeInEffects(float time = -1)
    {
        if (time >= 0)
            fadeTime = time;
        timeElapsed = 0;
        fadeIn = true;
        motionS.active = true;
        StartCoroutine(StartToggle());
    }

    private IEnumerator StartToggle()
    {
        yield return new WaitForSeconds(toggleTime);
        ToggleEffect(true);
    }

    private void OnDestroy()
    {
        filmGrainS.intensity.Override(filmIntenMax);

        lensDistortS.intensity.Override(lensIntenMax);
        lensDistortS.xMultiplier.Override(lensXMultMax);
        lensDistortS.scale.Override(lensScaleMax);

        vignetteS.intensity.Override(vignIntenMax);

        depthS.gaussianStart.Override(depthStartMax);

        colorAdjustS.postExposure.Override(colorAdjustPostMax);
        colorAdjustS.contrast.Override(colorAdjustConstrastMax);
        colorAdjustS.saturation.Override(colorAdjustSatMax);

        colorCurveS.active = true;
    }
}
