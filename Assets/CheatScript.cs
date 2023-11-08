using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatScript : MonoBehaviour
{
    public void CheatMoney()
    {
        PlayerData.Instance.AddMoney(999999);
    }
    public void ResetData()
    {
        PlayerData.Instance.ResetData();
    }
}
