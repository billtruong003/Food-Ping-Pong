using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy() {
        Instance = null;
    }
    [SerializeField] private PlayerDataSO playerData;

    public void AddMoney(int money)
    {
        playerData.AddMoney(money);
    }
    public string GetMoney()
    {
        return playerData.GetMoney();
    }
}
