using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI money;

    [Button]
    public void MoneyUpdate(int totalMoney)
    {
        string formattedMoney = totalMoney.ToString("N0");
        formattedMoney = formattedMoney.Replace(",", ".");
        Debug.Log(formattedMoney);
        money.text = formattedMoney;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
