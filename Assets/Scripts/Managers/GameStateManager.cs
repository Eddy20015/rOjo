using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    // using this as a GameStateManager and SceneManager
    private static GameStateManager Instance;
    private static GAMESTATE GameState;

    [SerializeField] private string MainMenuNameSetter;
    private static string MainMenuName;

    // current GameState options
    public enum GAMESTATE
    {
        PLAYING,
        PAUSED,
        MAINMENU,
        GAMEOVER,
        CINEMATIC,
        WIN
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            GameState = GAMESTATE.MAINMENU;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this);
        }

        MainMenuName = MainMenuNameSetter;
    }

    // returns current GameState
    public static GAMESTATE GetGameState()
    {
        return GameState;
    }


    // set GameState to PLAYING, would be used to resume play after a pause
    public static void Play()
    {
        GameState = GAMESTATE.PLAYING;
        Time.timeScale = 1f;
    }

    // sets GameState to PAUSE
    public static void Pause()
    {
        GameState = GAMESTATE.PAUSED;
        Time.timeScale = 0f;
    }

    // sets GameState to MAINMENU, loads main menu scene
    public static void MainMenu()
    {
        GameState = GAMESTATE.MAINMENU;
        SceneManager.LoadScene(MainMenuName);
        Time.timeScale = 1f;
    }

    // sets GameState to GAMEOVER
    public static void GameOver()
    {
        GameState = GAMESTATE.GAMEOVER;
        Time.timeScale = 0f;
    }

    // sets GameState to CINEMATIC
    public static void Cinematics()
    {
        GameState = GAMESTATE.CINEMATIC;
        Time.timeScale = 1f;
    }

    // sets GameState to WIN
    public static void Win()
    {
        GameState = GAMESTATE.WIN;
        Time.timeScale = 1f;
    }
}
