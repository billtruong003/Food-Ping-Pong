using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Data")]
public class PlayerDataSO : ScriptableObject
{
    public WeapID pickedWeap;
    public int money;
    public List<WeapID> unlockedWeapons;

    public int GetMoneyInt()
    {
        return money;
    }
    public void AddMoney(int money)
    {
        this.money += money;
        PlayerData.Instance.PrefSaveMoney();
    }
    public void DecreaseMoney(int moneyDec)
    {
        money -= moneyDec;
        PlayerData.Instance.PrefSaveMoney();
    }
    public string GetMoney()
    {
        string formattedMoney = money.ToString("N0");
        formattedMoney = formattedMoney.Replace(",", ".");
        return $"{formattedMoney}VND";
    }
    public void UnlockWeap(WeapID weap)
    {
        unlockedWeapons.Add(weap);
        PlayerData.Instance.PrefSaveUnlockWeap();
    }
    public WeapID GetPickedWeap()
    {
        return pickedWeap;
    }
    public List<WeapID> GetUnlockWeap()
    {
        return unlockedWeapons;
    }
    public void SetWeapUse(WeapID weap)
    {
        pickedWeap = weap;
        PlayerData.Instance.PrefSavePickWeap();
    }


    [Button]
    public void ResetData()
    {
        money = 0;
        unlockedWeapons.Clear();
        unlockedWeapons.Add(WeapID.KN001);
        pickedWeap = WeapID.KN001;
    }


}
