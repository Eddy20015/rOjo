using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AK.Wwise.Event clickSound;
    [SerializeField] AK.Wwise.Event specialClickSound;
    [SerializeField] AK.Wwise.Event stopMusic;
    public void OnClickNewGame()
    {
        stopMusic.Post(gameObject);
        specialClickSound.Post(gameObject);
        GameStateManager.NewGame();
    }

    public void OnClickQuitGame()
    {
        PlayClickSound();
        GameStateManager.QuitGame();
    }

    public void PlayClickSound()
    {
        clickSound.Post(gameObject);
    }
}
