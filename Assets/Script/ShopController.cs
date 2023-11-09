using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AirFishLab.ScrollingList;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class ShopController : MonoBehaviour
{
    [SerializeField] private WeapItem weapConfig;
    [SerializeField] private WeapInfo pickedItem;
    [SerializeField] private Transform containerShopItem;
    [SerializeField] private GameObject itemWeap;
    [SerializeField] private WeapID usingWeap;
    [SerializeField] private List<WeapID> unlockWeap;

    [Button]
    private void UpdateInfoItems()
    {
        StartCoroutine(UpdateInfo());
    }

    public void ChangePickedItem(WeapID id, WeapInfo script)
    {
        pickedItem.Unlocked();
        pickedItem = script;
        PlayerData.Instance.SetWeapUse(id);
    }
    private void Start()
    {
        UpdateInfoItems();
    }
    [Button]
    private void ClearItem()
    {
        if (containerShopItem.childCount == 0)
            return;
        foreach (Transform child in containerShopItem)
        {
            DestroyImmediate(child.gameObject);
        }
    }
    private IEnumerator UpdateInfo()
    {
        yield return new WaitUntil(() => PlayerData.Instance != null);
        for (int i = 0; i < weapConfig.GetItems(); i++)
        {
            int j = weapConfig.GetItems() - (1 + i);
            GameObject items = containerShopItem.GetChild(j).gameObject;
            items.name = $"item {j}";

            WeapInfo info = items.GetComponent<WeapInfo>();
            ItemShop item = weapConfig.GetItem(i);

            if (item.weapID == PlayerData.Instance.GetWeapInUse())
                pickedItem = info;

            info.SetInfo(item.itemImg, item.name, item.rarity.ToString(), item.price, item.weapID);
            info.SetRareColor(weapConfig.GetColor(item.rarity));
            info.SetShopController(this);
        }
    }
}
