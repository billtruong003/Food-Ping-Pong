using System.Collections;
using System.Collections.Generic;
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
    public IEnumerator GameplayScene ()
    {
        loadingScene.SetActive(true);
        yield return new WaitForSecondsRealtime(5);
        loadingScene.SetActive(false);
        MenuController.Instance.InitScene("Gameplay");
    }
}