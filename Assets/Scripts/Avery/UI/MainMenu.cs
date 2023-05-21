using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AK.Wwise.Event clickSound;
    [SerializeField] AK.Wwise.Event specialClickSound;
    public void OnClickNewGame()
    {
        specialClickSound.Post(gameObject);
        GameStateManager.NewGame();
    }

    public void OnClickQuitGame()
    {
        GameStateManager.QuitGame();
    }

    public void PlayClickSound()
    {
        clickSound.Post(gameObject);
    }
}
