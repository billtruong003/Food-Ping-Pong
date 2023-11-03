using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
public class RecipeController : MonoBehaviour
{
    [SerializeField] private GameObject cookItem;
    [SerializeField] private Transform cookInfo;
    [SerializeField] private Button cook;
    [SerializeField] private Transform recipeContainer;
    [SerializeField] private FoodDescription foodDescription;
    private List<string> lstMaterial;
    private ItemRecipe itemRecipe;

    public void CookInfoUpdate(Meal info)
    {

        ResetCookInfo();
        info.UpdateNumRecipes();
        lstMaterial = info.CookUse();
        List<Sprite> lstSpriteMat = info.LstSpriteMat();
        List<string> lstNameMat = info.lstNameMat();
        List<bool> enoughMat = new List<bool>();
        for (int i = 0; i < info.GetNumRecipes(); i++)
        {
            itemRecipe = recipeContainer.GetChild(i).GetComponent<ItemRecipe>();
            string itemCount = info.Recipes[i].GetItemCount();
            bool enough = info.Recipes[i].CheckCanCook();
            info.UpdateNumRecipes();
            itemRecipe.SetUpRecipes(lstSpriteMat[i], lstNameMat[i], itemCount, enough);
            itemRecipe.transform.gameObject.SetActive(true);
            enoughMat.Add(enough);
        }
        cookInfo.parent.gameObject.SetActive(true);
        cookInfo.gameObject.SetActive(true);

        if (enoughMat.Contains(false))
        {
            cook.interactable = false;
            return;
        }
        cook.interactable = true;
    }
    public void SetCookItem(GameObject item)
    {
        cookItem = item;
    }
    public void Cook()
    {
        //more logic here
        LevelManager.Instance.PlusTurn();
        LevelManager.Instance.CookApply(lstMaterial);
        Destroy(cookItem);
    }
    public void ResetCookInfo()
    {
        cookInfo.gameObject.SetActive(false);
        cookInfo.parent.gameObject.SetActive(false);
        foreach (Transform item in recipeContainer)
        {
            item.gameObject.SetActive(false);
        }
    }
    public void AppearDescription(string name, string description)
    {
        foodDescription.DescriptionUpdate(name, description);
    }
}
