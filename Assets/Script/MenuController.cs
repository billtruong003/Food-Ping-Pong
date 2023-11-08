using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI money;
    
    private void Start()
    {
        MoneyUpdate();
    }
    public static MenuController Instance;
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
    
    private IEnumerator Cor_MoneyUpdate()
    {
        yield return new WaitUntil(() => PlayerData.Instance != null);
        Debug.Log(PlayerData.Instance.GetMoney());
        money.text = PlayerData.Instance.GetMoney();
    }
    private void MoneyUpdate()
    {
        if (money == null)
        {
            return;
        }
        StartCoroutine(Cor_MoneyUpdate());
    }
    public void LoadScene(sceneName sceneName)
    {
        GameSceneController.Instance.LoadScene(sceneName);
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
            case ("Gameover"):
                LoadScene(sceneName.GAMEOVER);
                break;
            case ("Shop"):
                LoadScene(sceneName.SHOP);
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
