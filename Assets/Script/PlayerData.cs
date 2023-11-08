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
    private void OnDestroy()
    {
        Instance = null;
    }
    [SerializeField] private PlayerDataSO playerData;

    public void AddMoney(int money)
    {
        playerData.AddMoney(money);
        if (MenuController.Instance != null)
        {
            MenuController.Instance.MoneyUpdate();
        }
    }
    public int GetMoneyInt()
    {
        return playerData.GetMoneyInt();
    }
    public void DecreaseMoney(int money)
    {
        playerData.DecreaseMoney(money);
        if (MenuController.Instance != null)
        {
            MenuController.Instance.MoneyUpdate();
        }
    }
    public string GetMoney()
    {
        return playerData.GetMoney();
    }
    public WeapID GetWeapInUse()
    {
        return playerData.GetPickedWeap();
    }
    public void SetWeapUse(WeapID id)
    {
        playerData.SetWeapUse(id);
    }
    public List<WeapID> GetWeapUnlocked()
    {
        return playerData.GetUnlockWeap();
    }
    public void unlockItem(WeapID weap)
    {
        playerData.UnlockWeap(weap);
    }
    public void ResetData()
    {
        playerData.ResetData();
    }

}
