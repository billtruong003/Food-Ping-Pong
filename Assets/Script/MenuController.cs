using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    [SerializeField] private TextMeshProUGUI money;

    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    private void Start()
    {
        MoneyUpdate();
    }

    private IEnumerator Cor_MoneyUpdate()
    {
        yield return new WaitUntil(() => PlayerData.Instance != null);
        Debug.Log(money);
        if (money != null)
        {
            money.text = PlayerData.Instance.GetMoney();
        }
    }
    public void MoneyUpdate()
    {
        StartCoroutine(Cor_MoneyUpdate());
    }
    public void InitScene(string scene)
    {
        StartCoroutine(Cor_InitScene(scene));
    }
    public void ReloadScene()
    {
        StartCoroutine(Cor_ReloadGame());
    }

    public IEnumerator Cor_ReloadGame()
    {
        yield return new WaitUntil(() => (GameSceneController.Instance != null));
        GameSceneController.Instance.ReloadScene();
    }
    public IEnumerator Cor_InitScene(string scene)
    {
        yield return new WaitUntil(() => (GameSceneController.Instance != null));
        GameSceneController.Instance.LoadSceneFromString(scene);
    }
}
