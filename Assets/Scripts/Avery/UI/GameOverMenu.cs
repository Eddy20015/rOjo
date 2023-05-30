using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event StopAll;
    [SerializeField] private AK.Wwise.Event clickSound;
    void Awake()
    {
        DisableAllChildren();
    }

    public void OnClickRestart()
    {
        DisableAllChildren();
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
