using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndingUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] protected GameObject deathUI;
    [SerializeField] protected GameObject videoImage;

    [Header("Video")]
    [SerializeField] protected VideoPlayer vPlayer;
    [SerializeField] protected VideoClip cutscene;
    [SerializeField] protected VideoClip loopScreen;

    [Header("Audio")]
    [SerializeField] protected AK.Wwise.Event StopAll;

    private bool triggered;

    private void Awake()
    {
        ToggleUI();
        triggered = false;
    }

    public void PlayMenu()
    {
        if (triggered)
            return;
        StopAllCoroutines();
        triggered = true;
        StartCoroutine(PlayCutscene());
    }
    protected IEnumerator PlayCutscene()
    {
        // cutscene moment
        vPlayer.targetTexture.Release();
        GameStateManager.Cinematics();
        vPlayer.clip = cutscene;
        vPlayer.isLooping = false;
        vPlayer.Play();
        videoImage.SetActive(true);
        yield return new WaitForSeconds((float)cutscene.length);

        // loop screen moment
        vPlayer.Stop();
        vPlayer.clip = loopScreen;
        vPlayer.isLooping = true;
        vPlayer.Play();
        deathUI.SetActive(true);
        GameStateManager.CursorOn();
    }

    protected void ToggleUI(bool on = false)
    {
        deathUI.SetActive(on);
        videoImage.SetActive(on);
        if (!on)
            triggered = false;
    }

    public void MainMenuButton()
    {
        StopAll.Post(gameObject);
        GameStateManager.QuitToMainMenu();
    }

    public void ToCreditButton()
    {
        StopAll.Post(gameObject);
        GameStateManager.LoadCredits();
    }
}
