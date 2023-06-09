using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    // using this as a GameStateManager and SceneManager
    [Header("Scene Names")]
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

    private SceneTransition transition;

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

        AkSoundEngine.SetRTPCValue("Master_Volume", PlayerPrefs.GetFloat("MasterVolume", SettingsMenu.DEFAULT_VOLUME));
        AkSoundEngine.SetRTPCValue("Music_Volume", PlayerPrefs.GetFloat("MusicVolume", SettingsMenu.DEFAULT_VOLUME));
        AkSoundEngine.SetRTPCValue("SFX_Volume", PlayerPrefs.GetFloat("SFXVolume", SettingsMenu.DEFAULT_VOLUME));
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
        Instance.StopAllCoroutines();
        Instance.StartCoroutine(LoadLevelAsync(MainLevelName));
        //SceneManager.LoadScene(MainLevelName); // replace with async loading later
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
        Cinematics();
        Instance.StopAllCoroutines();
        Instance.StartCoroutine(TransitionToMain());
    }

    private static IEnumerator TransitionToMain()
    {
        Instance.transition = FindObjectOfType<SceneTransition>();
        Instance.transition.Close();
        yield return new WaitForSeconds(Instance.transition.CloseTime());
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

        Checkpoint.LoadCheckpoint();
        Play();
        if (Restarted != null)
            Restarted.Invoke();

        yield return new WaitForEndOfFrame();
    }

    public static void LoadCredits()
    {
        //Win();
        SceneManager.LoadScene(CreditsName);
    }

    public static IEnumerator LoadLevelAsync(string scene)
    {
        // clicking new game sets the checkpoint to the very beginning of the level:
        PlayerPrefs.SetFloat("checkpointX", -16f);
        PlayerPrefs.SetFloat("checkpointY", -3.25f);
        PlayerPrefs.SetFloat("checkpointZ", 0f);

        Instance.transition = FindObjectOfType<SceneTransition>();
        Instance.transition.Open();
        yield return new WaitForSeconds(Instance.transition.OpenTime());

        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        operation.allowSceneActivation = false;
        float timeElapsed = 0;
        float idleTime = Instance.transition.IdleTime();
        while (operation.progress < .9f || timeElapsed <= idleTime)
        {
            float progress = Mathf.Min(operation.progress, (timeElapsed / idleTime));
            Instance.transition.UpdateUI(progress);
            yield return new WaitForEndOfFrame();
            timeElapsed += Time.deltaTime;
        }

        Instance.transition.UpdateUI(1);
        Instance.transition.Close();
        yield return new WaitForSeconds(Instance.transition.CloseTime());
        operation.allowSceneActivation = true;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += LoadedMain;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadedMain;
    }

    private static void LoadedMain(Scene scene, LoadSceneMode mode)
    {
        print(scene.name + " loaded");
        if (scene.name == MainLevelName)
        {
            Checkpoint.LoadCheckpoint();
        }
    }
}