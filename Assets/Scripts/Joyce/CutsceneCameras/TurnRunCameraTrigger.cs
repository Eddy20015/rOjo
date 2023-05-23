using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRunCameraTrigger : CutCameraTrigger
{
    [Header("Camera Pass Off")]
    [SerializeField] private Transform HumanPlayer;
    [SerializeField] private Player3DMovement playerMoveScript;
    [SerializeField] private GameObject chaser;
    [SerializeField] private Player2DMovement dancerMovement;
    [SerializeField] private Animator bobbingCamAnim;

    [Header("Border")]
    [SerializeField] private Animator borderAnim;

    [Header("VFX")]
    [SerializeField] private PostProcessController ppc2D;
    //[SerializeField] private PostProcessController ppc3D;

    [Header("Sound")]
    [SerializeField] private AK.Wwise.Event Stop2DMusic;
    [SerializeField] private AK.Wwise.Event Start3DMusic;

    private void Awake()
    {
        chaser.SetActive(false);
        bobbingCamAnim.enabled = false;
    }
    protected override void StartTrigger()
    {
        if (borderAnim)
        {
            borderAnim.SetTrigger("ZoomOut");
        }
        switcher.SetPriority(newCamIndex);
        cutAnim.SetTrigger(cutSceneName);
        mainCam.orthographic = false;

        dancerMovement.enabled = false;

        Stop2DMusic.Post(gameObject);
        Start3DMusic.Post(gameObject);

        ppc2D.FadeOutEffects();

        StartCoroutine(OnCutSceneEnd());
    }
    protected override void EndTrigger()
    {
        StopAllCoroutines();
        switcher.SetPriority(0);
        cutAnim.SetTrigger("Idle");
        mainCam.orthographic = true;
    }
    protected override IEnumerator OnCutSceneEnd()
    {
        yield return new WaitForSeconds(cutSceneClip.length);

        switcher.ToggleAllCameras(false);
        mainCam.transform.SetParent(HumanPlayer);
        bobbingCamAnim.enabled = true;

        playerMoveScript.enabled = true;
        chaser.SetActive(true);
        dancerMovement.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Dancer"))
        {
            StartTrigger();
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag.Equals("Player"))
    //    {
    //        EndTrigger();
    //    }
    //}
}
