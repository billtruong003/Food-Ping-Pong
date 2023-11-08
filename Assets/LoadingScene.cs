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
        // spining.transform.Rotate(new Vector3 (0, 0, 45));
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
    public GameObject LoadingScreen;
    public Image LoadingBarFill;
    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        LoadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBarFill.fillAmount = progressValue;
            yield return null;
        }
    }
}
