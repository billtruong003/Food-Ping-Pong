using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    [SerializeField] private PlayerDataSO playerData;

    public void AddMoney(int money)
    {
        playerData.AddMoney(money);
    }
    public string GetMoney()
    {
        return playerData.GetMoney();
    }
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
}
