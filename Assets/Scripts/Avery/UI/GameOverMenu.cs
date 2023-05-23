using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event StopWind;
    [SerializeField] private AK.Wwise.Event Stop2DMusic;
    [SerializeField] private AK.Wwise.Event Stop3DMusic;
    void Awake()
    {
        DisableAllChildren();
    }

    public void OnClickRestart()
    {
        DisableAllChildren();
        StopWind.Post(gameObject);
        Stop2DMusic.Post(gameObject);
        Stop3DMusic.Post(gameObject);
        GameStateManager.Restart();
    }

    public void OnClickMainMenu()
    {
        StopWind.Post(gameObject);
        Stop2DMusic.Post(gameObject);
        Stop3DMusic.Post(gameObject);
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
