using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event StopAll;
    [SerializeField] private List<GameObject> toggleObjects;

    void Awake()
    {
        DisableAllChildren();
    }

    void Update()
    {
        // pausing and unpausing w/ escape key logic
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameStateManager.GetGameState() == GameStateManager.GAMESTATE.PLAYING)
            {
                //EnableAllChildren();
                ToggleOn();
                GameStateManager.Pause();
            }
            else if (GameStateManager.GetGameState() == GameStateManager.GAMESTATE.PAUSED)
            {
                OnClickResume();
            }
        }
    }

    public void OnClickResume()
    {
        DisableAllChildren();
        GameStateManager.ResumeGame();
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

    public void ToggleOn()
    {
        foreach (GameObject g in toggleObjects)
            g.SetActive(true);
    }
}
