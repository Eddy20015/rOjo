using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event StopWind;
    void Awake()
    {
        DisableAllChildren();
    }

    public void OnClickRestart()
    {
        DisableAllChildren();
        GameStateManager.Restart();
    }

    public void OnClickMainMenu()
    {
        StopWind.Post(gameObject);
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
}
