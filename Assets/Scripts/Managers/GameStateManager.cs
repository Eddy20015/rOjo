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

    [SerializeField] private string MainLevelNameSetter;
    private static string MainLevelName;

    [SerializeField] private string CreditsNameSetter;
    private static string CreditsName;

    public delegate void OnPlay();
    public static event OnPlay Restarted;

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
        MainLevelName = MainLevelNameSetter;
        CreditsName = CreditsNameSetter;
    }

    // returns current GameState
    public static GAMESTATE GetGameState()
    {
        return GameState;
    }



    // MAIN MENU OPTIONS
    // loads into the beginning of the game
    public static void NewGame()
    {
        EnemyVideoController.numDefaultSound = 0;
        EnemyVideoController.numIntensifiedSound = 0;
        SceneManager.LoadScene(MainLevelName); // replace with async loading later
        Play();
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    // PAUSE MENU OPTIONS
    // resumes game
    public static void ResumeGame()
    {
        Play();
    }

    // quits to main menu
    public static void QuitToMainMenu()
    {
        MainMenu();
    }

    //GAME OVER MENU OPTIONS
    public static void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Instance.StartCoroutine(ReloadSceneAsync(SceneManager.GetActiveScene().name));
    }



    // set GameState to PLAYING, would be used to resume play after a pause
    public static void Play()
    {
        GameState = GAMESTATE.PLAYING;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // sets GameState to PAUSE
    public static void Pause()
    {
        GameState = GAMESTATE.PAUSED;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // sets GameState to MAINMENU, loads main menu scene
    public static void MainMenu()
    {
        GameState = GAMESTATE.MAINMENU;
        SceneManager.LoadScene(MainMenuName);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // sets GameState to GAMEOVER
    public static void GameOver()
    {
        GameState = GAMESTATE.GAMEOVER;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // sets GameState to CINEMATIC
    public static void Cinematics()
    {
        GameState = GAMESTATE.CINEMATIC;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void CursorOn()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // sets GameState to WIN
    public static void Win()
    {
        GameState = GAMESTATE.WIN;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public static IEnumerator ReloadSceneAsync(string scene)
    {
        EnemyVideoController.numDefaultSound = 0;
        EnemyVideoController.numIntensifiedSound = 0;
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            yield return null;
        }
        Checkpoint.LoadCheckpoint();
        Play();
        if (Restarted != null)
            Restarted.Invoke();
    }

    public static void LoadCredits()
    {
        //Win();
        SceneManager.LoadScene(CreditsName);
    }

    //public IEnumerator LoadLevelAsync()
    //{

    //}
}
