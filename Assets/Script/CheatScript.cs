using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatScript : MonoBehaviour
{
    public void CheatMoney(int money)
    {
        PlayerData.Instance.AddMoney(money);
    }
    public void ResetData()
    {
        PlayerData.Instance.ResetData();
    }
}
