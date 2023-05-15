using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class PostProcessController : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField] protected VolumeProfile vProfile;

    public virtual void FadeInEffects() { }
    public virtual void FadeOutEffects() { }
}
