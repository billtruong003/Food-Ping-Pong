using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
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
    [Header("Weapon Count")]
    [SerializeField] private int weaponTurn;
    [SerializeField] private int playerTurnLeft;
    [SerializeField] private int gameTurn = 1;
    [Header("Money")]
    [SerializeField] private int money;

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
    public void SetTurn()
    {
        weaponTurn = 3;
        playerTurnLeft = 3;
        UIManager.Instance.SetCountWeaponTurn(playerTurnLeft);
    }
    public void AddMoney(int money)
    {
        this.money += money;
    }
    public int GetMoney()
    {
        return this.money;
    }
    public int GetGameTurn()
    {
        return gameTurn;
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
        int turnNum = Random.Range(2, 5);
        turn = turnNum;
    }
    public void DecreaseTurn()
    {
        UIManager.Instance.DecreaseWeaponCount(weaponTurn);
        gameTurn++;
        turn--;
        weaponTurn--;
        if (weaponTurn == 0)
        {
            playerTurnLeft--;
            if (playerTurnLeft < 0)
            {
                MainManager.Instance.InitState(2);
                Debug.Log("Game Over");
                return;
            }
            else
            {
                weaponTurn = 3;
                UIManager.Instance.ResetWeaponCount();
            }
            UIManager.Instance.SetCountWeaponTurn(playerTurnLeft);
        }
        if (turn != 0)
            return;

        SpawnFoodItem();
    }
    public void PlusTurn()
    {
        Debug.Log("Cộng turn");
        ResetTurn();
        UIManager.Instance.SetCountWeaponTurn(playerTurnLeft);
    }
    public void ResetTurn()
    {
        playerTurnLeft++;
        weaponTurn = 3;
        UIManager.Instance.ResetWeaponCount();
    }
    private void SpawnFoodItem()
    {
        Meal meal = MainManager.Instance.GetMeal();
        GameObject order = Instantiate(orderItem, orderContainer);
        RecipeInfo MealInfo = order.GetComponent<RecipeInfo>();
        MealInfo.SetMealInfo(meal);
        InitTurn();
    }
}
