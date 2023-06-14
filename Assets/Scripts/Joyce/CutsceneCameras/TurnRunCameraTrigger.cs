using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TurnRunCameraTrigger : CutCameraTrigger
{
    [Header("Cutscene")]
    [SerializeField] private PlayableDirector cutscenePlayer;
    [SerializeField] private GameObject eyeFlashPlayer;
    [SerializeField] private float cutsceneTime = 5f;

    [Header("Camera Pass Off")]
    [SerializeField] private Transform HumanPlayer;
    [SerializeField] private Player3DMovement playerMoveScript;
    [SerializeField] private GameObject chaser;
    [SerializeField] private Player2DMovement dancerMovement;
    [SerializeField] private Animator bobbingCamAnim;
    [SerializeField] private GameObject pauseMenu;

    [Header("Border")]
    [SerializeField] private Animator borderAnim;

    [Header("VFX")]
    [SerializeField] private PostProcessController ppc2D;
    [SerializeField] private PostProcessController ppc3D;

    [Header("Sound")]
    [SerializeField] private AK.Wwise.Event Stop2DMusic;
    [SerializeField] private AK.Wwise.Event Start3DMusic;

    private void Awake()
    {
        chaser.SetActive(false);
        bobbingCamAnim.enabled = false;
        eyeFlashPlayer.SetActive(false);
    }
    protected override void StartTrigger()
    {
        if (borderAnim)
        {
            borderAnim.SetTrigger("ZoomOut");
        }
        // play cutscne here an have everything else on a coroutine
        switcher.SetPriority(newCamIndex); // switches to the dolly camera
        mainCam.orthographic = false;
        dancerMovement.StopMoving();
        dancerMovement.enabled = false;
        eyeFlashPlayer.SetActive(true);

        cutscenePlayer.Play();
        /*
        switcher.SetPriority(newCamIndex);
        cutAnim.SetTrigger(cutSceneName);
        mainCam.orthographic = false;
        */

        Stop2DMusic.Post(gameObject);
        Start3DMusic.Post(gameObject);

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
        ppc2D.FadeOutEffects(cutSceneClip.length);

        if(ppc3D)
            ppc3D.FadeInEffects(cutSceneClip.length);

        yield return new WaitForSeconds(cutsceneTime);
        
        cutAnim.SetTrigger(cutSceneName);

        yield return new WaitForSeconds(cutSceneClip.length);
        cutscenePlayer.Stop();

        switcher.ToggleAllCameras(false);
        mainCam.transform.SetParent(HumanPlayer);
        bobbingCamAnim.enabled = true;
        playerMoveScript.enabled = true;
        chaser.SetActive(true);
        dancerMovement.enabled = false;
        pauseMenu.SetActive(false);
        
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
