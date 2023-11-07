using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleScene : MonoBehaviour
{
    public void SceneHandle(string name)
    {
        MenuController.Instance.InitScene(name);
    }
    public void RestartScene()
    {
        MenuController.Instance.ReloadScene();
    }
}
