using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI money;

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
}
