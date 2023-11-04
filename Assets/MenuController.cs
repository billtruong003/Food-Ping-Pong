using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    // public void LoadScene(sceneName sceneName)
    // {
    //     // GameSceneController.Instance.LoadScene(sceneName);
    // }
    public void BtnClickScene(sceneName sceneName)
    {
        GameSceneController.Instance.LoadScene(sceneName);
    }
    public void InitScene(string scene)
    {
        // yield return WaitUntil(() => (SceneManager.Instance != null));
        switch (scene)
        {
            case ("Menu"):
                GameSceneController.Instance.LoadScene(sceneName.MENU);
                break;
            case ("Weapon"):
                GameSceneController.Instance.LoadScene(sceneName.WEAPON);
                break;
            case ("Gameplay"):
                GameSceneController.Instance.LoadScene(sceneName.GAMEPLAY);
                break;
            case ("Gameover"):
                GameSceneController.Instance.LoadScene(sceneName.GAMEOVER);
                break;
            case ("Shop"):
                GameSceneController.Instance.LoadScene(sceneName.SHOP);
                break;
            case ("Setting"):
                GameSceneController.Instance.LoadScene(sceneName.SETTING);
                break;
            case ("Credit"):
                GameSceneController.Instance.LoadScene(sceneName.CREDIT);
                break;
            default:
                break;
        }
    }
}
