using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRunCameraTrigger : CutCameraTrigger
{
    [Header("Camera Pass Off")]
    [SerializeField] private Transform HumanPlayer;
    [SerializeField] private Player3DMovement playerMoveScript;
    protected override void StartTrigger()
    {
        switcher.SetPriority(newCamIndex);
        cutAnim.SetTrigger(cutSceneName);
        mainCam.orthographic = false;
        StartCoroutine(OnCutSceneEnd());
    }
    protected override void EndTrigger()
    {
        switcher.SetPriority(0);
        cutAnim.SetTrigger("Idle");
        mainCam.orthographic = true;
    }
    protected override IEnumerator OnCutSceneEnd()
    {
        yield return new WaitForSeconds(cutSceneClip.length);
        switcher.ToggleAllCameras(false);
        mainCam.transform.SetParent(HumanPlayer);
        playerMoveScript.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            StartTrigger();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            EndTrigger();
        }
    }
}
