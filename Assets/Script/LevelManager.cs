using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private List<string> inventory;
    [SerializeField] private int turn;

    [Header("Spawn Order")]
    [SerializeField] private Transform orderContainer;
    [SerializeField] private GameObject orderItem;

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
    private void Start()
    {
        SpawnFoodItem();

    }
    public void InventoryAdd(string material)
    {
        inventory.Add(material);
        inventory.Sort();
    }

    public void CookApply(List<string> materialUsed)
    {
        for (int i = 0; i < materialUsed.Count; i++)
        {
            RemoveNameInInventory(materialUsed[i]);
            UIManager.Instance.DecreaseMaterial(materialUsed[i]);
        }
    }

    [Button]
    public void ClearInventory()
    {
        inventory.Clear();
    }
    public int CheckInventory(string matCheck)
    {
        int count = 0;
        foreach (string item in inventory)
        {
            if (item == matCheck)
            {
                count++;
            }
        }
        return count;
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
    private void InitTurn()
    {
        int turnNum = Random.Range(2, 4);
        turn = turnNum;
    }
    public void DecreaseTurn()
    {
        turn--;
        if (turn != 0)
            return;

        SpawnFoodItem();
    }
    private void SpawnFoodItem()
    {
        Meal meal = GameManager.Instance.GetMeal();
        GameObject order = Instantiate(orderItem, orderContainer);
        RecipeInfo MealInfo = order.GetComponent<RecipeInfo>();
        MealInfo.SetMealInfo(meal);
        InitTurn();
    }
}
