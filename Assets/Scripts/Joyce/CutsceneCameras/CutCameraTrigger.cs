using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutCameraTrigger : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] protected Camera mainCam;
    [SerializeField] protected Animator cutAnim;

    [Header("Switching")]
    [SerializeField] protected CinemachineSwitcher switcher;
    [SerializeField] protected int newCamIndex = 1;
    [SerializeField] protected float transitionTime = 2f;

    [Header("Cutscene")]
    [SerializeField] protected AnimationClip cutSceneClip;
    [SerializeField] protected string cutSceneName;

    protected bool playingCutscene = false;

    virtual protected void StartTrigger(){}
    virtual protected void EndTrigger(){}

    virtual protected IEnumerator OnCutSceneEnd()
    {
        yield return new WaitForSeconds(cutSceneClip.length);
        // do something here lmao
    }
}
