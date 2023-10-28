using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] private string currentScene;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadScene(sceneName scene)
    {
        string sceneLoad = GetSceneName(scene);
    }
    public string GetSceneName(sceneName scene)
    {
        switch (scene)
        {
            case (sceneName.MENU):
                return "Menu";
            case (sceneName.GAMEPLAY):
                return "GamePlay";
            case (sceneName.SETTING):
                return "Setting";
            case (sceneName.CREDIT):
                return "Credit";
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
    SETTING,
    CREDIT,

}