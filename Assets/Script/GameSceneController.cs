using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    public static GameSceneController Instance;
    [SerializeField] private string currentScene;
    public GameObject LoadingScreen;
    private float lastSoundVol;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;
    }
    public void LoadScene(sceneName scene)
    {
        if (scene == sceneName.GAMEPLAY && PlayerData.Instance.NewPlay)
        {
            StartCoroutine(LoadSceneAsync(sceneName.TUTORIAL));
            return;
        }
        StartCoroutine(LoadSceneAsync(scene));
    }

    IEnumerator LoadSceneAsync(sceneName sceneId)
    {
        string sceneLoad = GetSceneName(sceneId);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneLoad, LoadSceneMode.Single);
        LoadingScreen.SetActive(true);
        yield return new WaitUntil(() => operation.isDone);
        LoadingScreen.SetActive(false);
    }
    public string GetSceneName(sceneName scene)
    {
        switch (scene)
        {
            case (sceneName.MENU):
                return "MenuGame";
            case (sceneName.GAMEPLAY):
                return "GamePlay";
            case (sceneName.SETTING):
                return "SettingScene";
            case (sceneName.CREDIT):
                return "CreditScene";
            case (sceneName.WEAPON):
                return "ChooseWeap";
            case (sceneName.TUTORIAL):
                return "Tutorial";
        }
        return currentScene;
    }
    public void LoadSceneFromString(string scene)
    {
        switch (scene)
        {
            case ("Menu"):
                SetMusicVolNorm();
                LoadScene(sceneName.MENU);
                break;
            case ("Weapon"):
                SetMusicVolNorm();
                LoadScene(sceneName.WEAPON);
                break;
            case ("Gameplay"):
                SetMusicVolInGame(20);
                LoadScene(sceneName.GAMEPLAY);
                break;
            case ("Setting"):
                SetMusicVolNorm();
                LoadScene(sceneName.SETTING);
                break;
            case ("Credit"):
                SetMusicVolNorm();
                LoadScene(sceneName.CREDIT);
                break;
            case ("Tutorial"):
                SetMusicVolNorm();
                LoadScene(sceneName.TUTORIAL);
                break;
            default:
                break;
        }
    }
    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void SetMusicVolInGame(float musicVol)
    {
        if (SoundManager.Instance != null)
        {
            lastSoundVol = SoundManager.Instance.GetMusicVol();
            SoundManager.Instance.SetMusicVol(musicVol);
        }
    }
    public void SetMusicVolNorm()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetMusicVol(lastSoundVol);
        }
    }

}
[Serializable]
public enum sceneName
{
    NONE,
    MENU,
    GAMEPLAY,
    WEAPON,
    SETTING,
    CREDIT,
    TUTORIAL,
}