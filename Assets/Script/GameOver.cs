using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private MenuController sceneController;
    public void MoneyUpdate(int totalMoney)
    {
        string formattedMoney = totalMoney.ToString("N0");
        formattedMoney = formattedMoney.Replace(",", ".");
        Debug.Log(formattedMoney);
        money.text = formattedMoney;
    }
    public void TriggerGameOject()
    {
        MoneyUpdate(LevelManager.Instance.GetMoney());
        gameObject.SetActive(true);
        StartCoroutine(SetUpData());
    }
    public IEnumerator SetUpData()
    {
        yield return new WaitUntil(() => PlayerData.Instance != null);
        PlayerData.Instance.AddMoney(LevelManager.Instance.GetMoney());
    }
    public void LoadMenu()
    {
        sceneController.InitScene("Menu");
    }
    public void ReloadScene()
    {
        sceneController.ReloadScene();
    }
}
