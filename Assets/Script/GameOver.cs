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
        gameObject.SetActive(true);
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
