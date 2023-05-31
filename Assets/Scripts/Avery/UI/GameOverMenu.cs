using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : EndingUI
{
    [SerializeField] private AK.Wwise.Event clickSound;

    private void OnEnable()
    {
        GameStateManager.Restarted += Reloaded;
    }
    private void OnDisable()
    {
        GameStateManager.Restarted -= Reloaded;
    }

    private void Reloaded()
    {
        ToggleUI(false);    
    }

    public void OnClickRestart()
    {
        //DisableAllChildren();
        StopAll.Post(gameObject);
        PlayClickSound();
        GameStateManager.Restart();
    }

    public void OnClickMainMenu()
    {
        StopAll.Post(gameObject);
        PlayClickSound();
        GameStateManager.QuitToMainMenu();
    }

    // disables all children of the Canvas object when pausing
    public void DisableAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    // enables all children of the Canvas object when unpausing
    public void EnableAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    public void PlayClickSound()
    {
        clickSound.Post(gameObject);
    }
}
