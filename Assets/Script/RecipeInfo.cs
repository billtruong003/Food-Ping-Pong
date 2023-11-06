using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeInfo : MonoBehaviour
{
    [SerializeField] private string mealName;
    [SerializeField] private string mealDescription;
    [SerializeField] private List<string> recipes;
    [SerializeField] private bool canCook;
    [Header("UI")]
    [SerializeField] private Image mealIMG;
    [SerializeField] private TextMeshProUGUI foodName;
    [SerializeField] private Meal mealInfo;

    private RecipeController recipeController;
    private void Start()
    {
        recipeController = GetComponentInParent<RecipeController>();
    }
    public void ShowRecipes()
    {
        recipeController.CookInfoUpdate(mealInfo);
        recipeController.SetCookItem(gameObject);
    }
    public void SetMealInfo(Meal info)
    {
        mealInfo = info;

        mealName = info.Food;
        mealDescription = info.Descriptions;

        recipes = info.CookUse();

        mealIMG.sprite = mealInfo.GetFoodIcon();
        foodName.text = mealName;
    }
    public int GetPrice()
    {
        return mealInfo.Money;
    }
    public void ShowDescription()
    {
        recipeController.AppearDescription(mealName, mealDescription);
    }
}
