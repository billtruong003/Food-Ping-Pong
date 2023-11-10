using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    public bool NewPlay = true;
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
            NewPlay = bool.Parse(PlayerPrefs.GetString("newplay", ""));
            LoadUnlockedWeap();
            Debug.Log("PlayerPrefs data loaded.");
        }
        else
        {
            PrefSaveMoney();
            PrefSavePickWeap();
            PrefSaveUnlockWeap();
            PrefSaveNewPlay();
            Debug.Log(playerData.money);
        }
    }
    public void PrefSaveUnlockWeap()
    {
        List<string> unlockedWeaponsStrings = new List<string>();
        foreach (WeapID weap in playerData.unlockedWeapons)
        {
            unlockedWeaponsStrings.Add(weap.ToString());
            Debug.Log($"Save Unlock Weap: {weap}");
        }
        string jsonPack = ListTostring(unlockedWeaponsStrings);
        PlayerPrefs.SetString("unlockedWeapons", jsonPack);
        Debug.Log($"UnlockWeaponsLoad: {PlayerPrefs.GetString("unlockedWeapons", "")}");
        PrefSaveAll();
    }
    [Button]
    public void LoadUnlockedWeap()
    {
        string savedUnlockedWeaponsJSON = PlayerPrefs.GetString("unlockedWeapons", "");
        Debug.Log($"UnlockWeaponsLoad: {savedUnlockedWeaponsJSON}");

        playerData.unlockedWeapons = new List<WeapID>();

        List<string> savedUnlockedWeaponsStrings = StringToLst(savedUnlockedWeaponsJSON);

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
    public void PrefSaveNewPlay()
    {
        PlayerPrefs.SetString("newplay", NewPlay.ToString());
        PrefSaveAll();
    }

    public void PrefSavePickWeap()
    {
        PlayerPrefs.SetString("pickWeap", playerData.pickedWeap.ToString());
        Debug.Log($"Save pick weap:{playerData.pickedWeap.ToString()}");
        PrefSaveAll();
    }
    public string ListTostring(List<string> lst)
    {
        string result = string.Join(", ", lst);
        Debug.Log(result);
        return result;
    }
    public List<string> StringToLst(string lst)
    {

        string[] elements = lst.Split(new string[] { ", " }, StringSplitOptions.None);

        List<string> myList = new List<string>(elements);

        foreach (var item in myList)
        {
            Debug.Log(item);
        }
        return myList;
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
