using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class KeyFader : MonoBehaviour
{
    [Header("Red Keyer")]
    [SerializeField] private Material redKeyer;
    [SerializeField] private float redCutoffStart = .6f;
    [SerializeField] private float redCutoffEnd = 0;
    [SerializeField] private float redFadeTime = 3;
    [SerializeField] private float startSpeed = .1f;
    private float acceleration;

    [Header("Rippler")]
    [SerializeField] private Material ripple;
    [SerializeField] private float rippleCountStart = 0;
    [SerializeField] private float rippleCountEnd = .8f;
    [SerializeField] private float rippleFadeTime = 3;
    private float timeElapsed;

    private SpriteRenderer sr;

    private void Awake()
    {
        timeElapsed = 0;
        sr = GetComponent<SpriteRenderer>();
        SetMats();
        sr.material = redKeyer;
        StartCoroutine(FadeBG());
    }

    private void SetMats()
    {
        redCutoffStart = redKeyer.GetFloat("_ColorCutoff");
        rippleCountStart = ripple.GetFloat("_ripplecount");
        acceleration = (1 - startSpeed) / redFadeTime;
    }
    private IEnumerator FadeBG()
    {
        timeElapsed = 0;
        while (timeElapsed <= redFadeTime)
        {
            redKeyer.SetFloat("_ColorCutoff", Mathf.Lerp(redCutoffStart, redCutoffEnd, timeElapsed / redFadeTime));
            yield return new WaitForEndOfFrame();
            startSpeed += Time.deltaTime * acceleration;
            timeElapsed += Time.deltaTime * startSpeed;
        }

        timeElapsed = 0;
        sr.material = ripple;
        while (timeElapsed <= rippleFadeTime)
        {
            ripple.SetFloat("_ripplecount", Mathf.Lerp(rippleCountStart, rippleCountEnd, timeElapsed / rippleFadeTime));
            yield return new WaitForEndOfFrame();
            timeElapsed += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        redKeyer.SetFloat("_ColorCutoff", redCutoffStart);
        ripple.SetFloat("_ripplecount", rippleCountStart);
    }
}
