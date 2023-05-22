using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAnimatorController : MonoBehaviour
{
    [SerializeField] protected Animator VPlayer;

    protected bool bContinue;

    protected virtual void Start()
    {
        print("In the old start");
        VPlayer = gameObject.GetComponent<Animator>();
    }

    protected void StartVPlayer()
    {
        VPlayer.StartPlayback();
    }

    protected void SetTime(float time)
    {
        float myTime = time / VPlayer.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        VPlayer.Play(VPlayer.GetCurrentAnimatorClipInfo(0)[0].clip.name, 0, myTime);
    }

    protected float GetTime()
    {
        return VPlayer.GetCurrentAnimatorStateInfo(0).normalizedTime * VPlayer.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }
    
    protected float GetTotalTime()
    {
        return VPlayer.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    protected void PauseVPlayer()
    {
        ClipSpeed(0); //No Pause for VPlayer
    }

    protected void StopVPlayer()
    {
        VPlayer.enabled = false;
    }

    protected void ClipSpeed(int Speed)
    {
        VPlayer.speed = Speed;
    }

    protected void CheckOver()
    {
        bContinue = true;
    }
}
