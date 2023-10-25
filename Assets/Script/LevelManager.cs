using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private List<string> inventory;
    [SerializeField] private int pickUpLeft;


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
    public void InventoryAdd(string material)
    {
        inventory.Add(material);
        inventory.Sort();
    }

    public void IncreasePickUp()
    {
        pickUpLeft++;
    }

    public void DecreasePickUp()
    {
        pickUpLeft--;
    }
    public void CheckPickUpLeft()
    {
        if (pickUpLeft == 0)
        {

        }
    }
    public void CookInventory(List<string> materialUsed)
    {
        for (int i = 0; i < materialUsed.Count; i++)
        {
            RemoveNameInInventory(materialUsed[i]);
        }
    }

    [Button]
    public void ClearInventory()
    {
        inventory.Clear();
    }
    public void RemoveNameInInventory(string name)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (name == inventory[i])
            {
                inventory.RemoveAt(i);
                break;
            }
        }
    }    
}
