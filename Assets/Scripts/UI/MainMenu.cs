using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnClickNewGame()
    {
        GameStateManager.NewGame();
    }

    public void OnClickQuitGame()
    {
        GameStateManager.QuitGame();
    }
}
