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