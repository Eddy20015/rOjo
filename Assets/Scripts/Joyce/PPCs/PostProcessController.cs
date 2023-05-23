using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class PostProcessController : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField] protected VolumeProfile vProfile;

    [Header("Fade Controls")]
    [SerializeField] protected float fadeTime; 
    protected float timeElapsed;

    public virtual void FadeInEffects(float time = -1) { }
    public virtual void FadeOutEffects(float time = -1) { }

    public virtual void ToggleEffect(bool on) { }
}
