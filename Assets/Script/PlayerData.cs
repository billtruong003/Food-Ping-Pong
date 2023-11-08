using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
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
        InitData();
    }
    private void InitData()
    {
        int savedMoney = PlayerPrefs.GetInt("money", 0);
        string savedPickWeap = PlayerPrefs.GetString("pickWeap", "");
        string savedUnlockedWeaponsJSON = PlayerPrefs.GetString("unlockedWeapons", "");

        if (savedMoney != 0 || savedPickWeap != "" || savedUnlockedWeaponsJSON != "")
        {
            playerData.money = savedMoney;
            playerData.pickedWeap = (WeapID)Enum.Parse(typeof(WeapID), savedPickWeap);
            LoadUnlockedWeap();
            Debug.Log("PlayerPrefs data loaded.");
        }
        else
        {
            PrefSaveMoney();
            PrefSavePickWeap();
            PrefSaveUnlockWeap();
            Debug.Log(playerData.money);
        }
    }
    public void PrefSaveUnlockWeap()
    {
        List<string> unlockedWeaponsStrings = new List<string>();
        foreach (WeapID weap in playerData.unlockedWeapons)
        {
            unlockedWeaponsStrings.Add(weap.ToString());
        }
        string unlockedWeaponsJSON = JsonUtility.ToJson(unlockedWeaponsStrings);
        PlayerPrefs.SetString("unlockedWeapons", unlockedWeaponsJSON);
        PrefSaveAll();
    }
    [Button]
    public void LoadUnlockedWeap()
    {
        string savedUnlockedWeaponsJSON = PlayerPrefs.GetString("unlockedWeapons", "");
        Debug.Log(savedUnlockedWeaponsJSON);
        playerData.unlockedWeapons = new List<WeapID>();
        List<string> savedUnlockedWeaponsStrings = JsonUtility.FromJson<List<string>>(savedUnlockedWeaponsJSON);

        foreach (string weapString in savedUnlockedWeaponsStrings)
        {
            WeapID weapID;
            Debug.Log(weapString);
            if (Enum.TryParse(weapString, out weapID))
            {
                playerData.unlockedWeapons.Add(weapID);
            }
        }
        foreach (WeapID id in playerData.unlockedWeapons)
        {
            Debug.Log(id);
        }
        PrefSaveAll();
    }
    public void PrefSaveMoney()
    {
        PlayerPrefs.SetInt("money", playerData.money);
        PrefSaveAll();
    }
    public void PrefSavePickWeap()
    {
        PlayerPrefs.SetString("pickWeap", playerData.pickedWeap.ToString());
        PrefSaveAll();
    }

    public void PrefSaveAll()
    {
        PlayerPrefs.Save();
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
