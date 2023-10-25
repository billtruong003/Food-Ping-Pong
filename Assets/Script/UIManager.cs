using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    [SerializeField] PickUpSpawner pickUpSpawner;
    [SerializeField] private PickUpConfig gameConfig;
    [SerializeField] private GameObject inventList;
    [SerializeField] private GameObject inventMenu;
    [SerializeField] private Transform inventorySlot;

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
    public void DecreaseMaterial(string name)
    {
        foreach (Transform child in inventorySlot)
        {
            MaterialSlot matSlot = child.GetComponent<MaterialSlot>();
            if (matSlot.MatName == name)
            {
                matSlot.MaterialDecreased();
                break;
            }
        }
    }
    public void CheckInventory(string matName)
    {
        foreach(Transform slot in inventorySlot)
        {
            MaterialSlot matSlot = slot.GetComponent<MaterialSlot>();
            if (matSlot.MatName == "")
            {
                matSlot.MaterialTrigger(matName);
                return;
            }
            if (matSlot.MatName == matName)
            {
                matSlot.MaterialAdded();
                return;
            }
        }
    }
}
