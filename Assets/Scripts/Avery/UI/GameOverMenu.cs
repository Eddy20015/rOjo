using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : EndingUI
{
    [SerializeField] private AK.Wwise.Event clickSound;
    [SerializeField] private AK.Wwise.Event stopLoops;

    private void Reloaded()
    {
        ToggleUI(false);    
    }

    public void OnClickRestart()
    {
        //DisableAllChildren();
        stopLoops.Post(gameObject);
        ToggleUI(false);
        GameStateManager.Restart();
    }

    public void OnClickMainMenu()
    {
        StopAll.Post(gameObject);
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
