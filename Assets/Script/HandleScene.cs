using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleScene : MonoBehaviour
{
    public void SceneHandle(string name)
    {
        InitScene(name);
    }
    public void RestartScene()
    {
        MenuController.Instance.ReloadScene();
    }
    public void LoadScene(sceneName sceneName)
    {
        GameSceneController.Instance.LoadScene(sceneName);
    }
    public void LoadSceneFromString(string sceneName)
    {
        GameSceneController.Instance.LoadSceneFromString(sceneName);
    }
    public void InitScene(string scene)
    {
        StartCoroutine(Cor_InitScene(scene));
    }
    public IEnumerator Cor_InitScene(string scene)
    {
        yield return new WaitUntil(() => (GameSceneController.Instance != null));
        switch (scene)
        {
            case ("Menu"):
                LoadScene(sceneName.MENU);
                break;
            case ("Weapon"):
                LoadScene(sceneName.WEAPON);
                break;
            case ("Gameplay"):
                LoadScene(sceneName.GAMEPLAY);
                break;
            case ("Setting"):
                LoadScene(sceneName.SETTING);
                break;
            case ("Credit"):
                LoadScene(sceneName.CREDIT);
                break;
            default:
                break;
        }
    }
}
