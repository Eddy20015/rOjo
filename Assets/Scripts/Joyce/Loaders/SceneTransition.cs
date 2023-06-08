using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected GameObject screen;

    [Header("Animation Clips Lengths")]
    [SerializeField] private float openLength = 1f;
    [SerializeField] private float idleTime = 2f;
    [SerializeField] private float closeLength = 1f;

    private void Awake()
    {
        screen.SetActive(false);
    }

    public float OpenTime()
    {
        return openLength; 
    }
    
    public float IdleTime()
    {
        return idleTime; 
    }

    public float CloseTime()
    {
        return closeLength;
    }

    public virtual void Open()
    {
        screen.SetActive(true);
        anim.SetTrigger("Open");
    }

    public virtual void Close()
    {
        anim.SetTrigger("Close");
    }

    public virtual void UpdateUI(float progress) { } // base class does nothingggggg
}
