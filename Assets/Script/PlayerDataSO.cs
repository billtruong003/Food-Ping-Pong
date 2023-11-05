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
    public string GetMoney()
    {
        string formattedMoney = money.ToString("N0");
        formattedMoney = formattedMoney.Replace(",", ".");
        return $"{formattedMoney}VND";
    }
    public void SetWeap(WeapID weap)
    {
        unlockedWeapons.Add(weap);
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