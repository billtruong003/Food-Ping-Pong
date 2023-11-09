using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private GameObject loadingScene, spinning;
    private void Update()
    {
        spinning.transform.Rotate(0, 0, -90 * Time.deltaTime);
    }
    public void PressBtnPlay()
    {
        StartCoroutine(GameplayScene());
    }
    public IEnumerator GameplayScene()
    {
        loadingScene.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        loadingScene.SetActive(false);
        MenuController.Instance.InitScene("Gameplay");
    }

}
