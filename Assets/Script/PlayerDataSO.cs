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
    public void AddMoney(int money)
    {
        this.money += money;
    }
    public int GetMoneyInt()
    {
        return money;
    }
    public void DecreaseMoney(int moneyDec)
    {
        money -= moneyDec;
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
    }
    public void ResetData()
    {
        money = 0;
        unlockedWeapons.Clear();
        unlockedWeapons.Add(WeapID.KN001);
        pickedWeap = WeapID.KN001;
    }


}
[Serializable]
public enum WeapID
{
    KN001,
    KN002,
    KN003,
    KN004,
    KN005,
    KN006,
    KN007,
    KN008,
    KN009,
}