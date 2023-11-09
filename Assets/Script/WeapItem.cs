using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopWeap", menuName = "ShopData/Data")]
public class WeapItem : ScriptableObject
{
    [SerializeField] private List<ItemShop> shopItems;
    [SerializeField] private List<Color> unique;
    [SerializeField] private Color normal;
    [SerializeField] private Color rare;
    [SerializeField] private Color sRare;
    [SerializeField] private Color superSRare;
    private const string weapCode = "KN";

    public int GetItems()
    {
        return shopItems.Count;
    }
    public ItemShop GetItem(int index)
    {
        return shopItems[index];
    }
    private ColorMode CheckRare(Rarity rare)
    {
        if (rare == Rarity.UNIQUE)
        {
            return ColorMode.FourCornersGradient;
        }
        else
        {
            return ColorMode.Single;
        }
    }
    public List<Color> GetColor(Rarity rarity)
    {
        switch (rarity)
        {
            case (Rarity.UNIQUE):
                return unique;
            case (Rarity.NORMAL):
                return new List<Color> { normal };
            case (Rarity.RARE):
                return new List<Color> { rare };
            case (Rarity.SRARE):
                return new List<Color> { sRare };
            case (Rarity.SSRARE):
                return new List<Color> { superSRare };
        }
        return new List<Color> { normal };
    }
    [Button]
    public void FillWeapID()
    {
        int i = 1;
        foreach (ItemShop item in shopItems)
        {
            item.SetWeapID($"{weapCode}{CountCodeNum(i)}");
            i++;
        }
    }
    private string CountCodeNum(int i)
    {
        if (i > 0 && i < 10)
        {
            return "00" + i.ToString();
        }
        else if (i >= 10 && i < 100)
        {
            return "0" + i.ToString();
        }
        return "001";
    }


}
[Serializable]
public class ItemShop
{
    public string name;
    public string price;
    public Rarity rarity;
    public WeapID weapID;
    public Sprite itemImg;

    public void SetWeapID(string id)
    {
        foreach (WeapID weapId in Enum.GetValues(typeof(WeapID)))
        {
            if (id == weapId.ToString())
            {
                Debug.Log($"{id} || {weapId.ToString()}");
                weapID = weapId;
            }
        }
    }
    public void SetWeapSprite(Sprite icon)
    {
        itemImg = icon;
    }
}
public enum Rarity
{
    UNIQUE,
    NORMAL,
    RARE,
    SRARE,
    SSRARE,
}
[Serializable]
public enum WeapID
{
    KN000,
    KN001,
    KN002,
    KN003,
    KN004,
    KN005,
    KN006,
    KN007,
    KN008,
    KN009,
    KN010,
    KN011,
    KN012,
    KN013,
    KN014,
    KN015,
}