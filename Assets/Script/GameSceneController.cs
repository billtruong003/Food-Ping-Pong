using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] private string currentScene;
    public static GameSceneController Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy() {
        Instance = null;
    }
    private void Start()
    {
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;
    }
    public void LoadScene(sceneName scene)
    {
        string sceneLoad = GetSceneName(scene);
        SceneManager.LoadScene(sceneLoad, LoadSceneMode.Single);
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