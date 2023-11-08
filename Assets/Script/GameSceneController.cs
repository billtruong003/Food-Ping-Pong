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
            case (sceneName.SHOP):
                return "ShopScene";
            case (sceneName.GAMEOVER):
                return "ShopScene";
        }
        return currentScene;
    }
    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
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
    SHOP,
    GAMEOVER
}