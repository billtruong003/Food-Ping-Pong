using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "MealConfig", menuName = "Food/Meals")]
public class PickUpConfig : ScriptableObject
{
    [SerializeField] public List<Meal> DishesInformation;
    [SerializeField] public List<RawMaterial> RawMaterials;
    [SerializeField] public List<Obstacle> Obstacles;
    [SerializeField] public List<Weapon> Weapons;
    private const string pickUpPath = "Food/Pickup";
    private const string WeapPath = "Weapon/";
    private const string MatPath = "RawMaterial/";
    private const string Food = "Food/";

    public GameObject GetPickUpMaterial(int index)
    {
        Transform pickUpPref = Resources.Load<Transform>(pickUpPath);
        PickUpItem pickUpName = pickUpPref.GetComponent<PickUpItem>();
        pickUpName.SetMatSprite(RawMaterials[index].GetSprite());
        pickUpName.SetMatName(RawMaterials[index].GetName());

        return pickUpName.gameObject;
    }
    public GameObject GetMaterialToCook(string name)
    {
        int index = 0;
        for (int i = 0; i < RawMaterials.Count; i++)
        {
            if (name == RawMaterials[i].GetName())
            {
                index = i;
                break;
            }
        }
        Transform pickUpPref = Resources.Load<Transform>(pickUpPath);
        PickUpItem pickUpName = pickUpPref.GetComponent<PickUpItem>();
        pickUpName.SetMatSprite(RawMaterials[index].GetSprite());
        pickUpName.SetMatName(RawMaterials[index].GetName());
        return pickUpName.gameObject;
    }
    public Meal GetRandomMeal()
    {
        int index = UnityEngine.Random.Range(0, DishesInformation.Count);
        return DishesInformation[index];
    }
    public Sprite GetSpriteMat(string name)
    {
        foreach (var rawMaterial in RawMaterials)
        {
            if (rawMaterial.GetName() == name)
            {
                return rawMaterial.GetSprite();
            }
        }
        return RawMaterials[0].GetSprite();
    }
    public string GetMatDescription(string name)
    {
        foreach (var rawMaterial in RawMaterials)
        {
            if (rawMaterial.GetName() == name)
            {
                return rawMaterial.GetDescription();
            }
        }
        return RawMaterials[0].GetDescription();
    }

    [Button]
    private void FillWeapPath()
    {
        foreach (var weapon in Weapons)
        {
            weapon.WeaponName = weapon.WeaponID.ToString();
            weapon.WeaponPath = WeapPath + weapon.WeaponName;
        }
    }

    public int GetRandomPickUp()
    {
        int rndPickUp = UnityEngine.Random.Range(0, RawMaterials.Count);
        return rndPickUp;
    }

    public GameObject GetWeapon(WeapID iD)
    {
        foreach (var weap in Weapons)
        {
            if (iD == weap.WeaponID)
            {
                return weap.GetWeaponPrefab();
            }
        }
        return null;
    }


}
[Serializable]
public class Meal
{
    [Header("Food")]
    public string Food;
    public Sprite FoodSprite;
    [Header("Infomation")]
    public int Money;
    [TextArea]
    public string Descriptions;

    [Header("Raw Material")]
    public List<Recipe> Recipes;
    public string NoticeInfo = "";
    public Sprite GetFoodIcon()
    {
        return FoodSprite;
    }
    public int GetMoney()
    {
        return Money;
    }
    public int GetNumRecipes()
    {
        return Recipes.Count;
    }
    public List<string> lstNameMat()
    {
        List<string> lstSprite = new List<string>();
        foreach (Recipe item in Recipes)
        {
            lstSprite.Add(item.MaterialName());
        }
        return lstSprite;
    }
    public List<Sprite> LstSpriteMat()
    {
        List<Sprite> lstSprite = new List<Sprite>();
        foreach (Recipe item in Recipes)
        {
            lstSprite.Add(MainManager.Instance.GetSpriteMaterial(item.MaterialName()));
        }
        return lstSprite;
    }
    public void UpdateNumRecipes()
    {
        foreach (Recipe item in Recipes)
        {
            item.UpdateItemHas(LevelManager.Instance.CheckInventory(item.MaterialName()));
        }
    }
    public bool checkMatForCook()
    {
        foreach (Recipe item in Recipes)
        {
            if (!item.CheckCanCook())
                return false;
        }
        return true;
    }
    public List<string> CookUse()
    {
        List<string> lstRecipes = new List<string>();
        for (int i = 0; i < GetNumRecipes(); i++)
        {
            int materialNum = Recipes[i].GetItemNum();
            Debug.Log($"num: {materialNum}");
            for (int j = 0; j < materialNum; j++)
            {
                lstRecipes.Add(Recipes[i].MaterialName());
            }
        }
        return lstRecipes;
    }
    [Serializable]
    public class Recipe
    {
        public string RawMaterialName;
        public int ItemContains = 0;
        private int itemHas = 0;
        private bool canCook = false;

        public void UpdateItemHas(int itemInInventory)
        {
            itemHas = itemInInventory;
            if (itemHas >= (ItemContains * CalculateItemNeed()))
            {
                UpdateCanCook(true);
                return;
            }
            UpdateCanCook(false);
        }
        public bool CheckCanCook()
        {
            return canCook;
        }
        public void UpdateCanCook(bool stat)
        {
            canCook = stat;
        }
        public int GetItemNum()
        {
            return ItemContains;
        }
        public string MaterialName()
        {
            return RawMaterialName;
        }
        public string GetItemCount()
        {
            return $"{itemHas}/{ItemContains * CalculateItemNeed()}";
        }
        public int CalculateItemNeed()
        {
            return (int)((LevelManager.Instance.GetGameTurn() / 10) + 1);
        }
    }
    [Serializable]
    public class HiddenRecipe
    {
        public string HiddenMaterialName;
        public int ItemContains = 0;

        public string GetItemNum()
        {
            return ItemContains.ToString();
        }
        public string MaterialName()
        {
            return ItemContains.ToString();
        }
    }
}

[Serializable]
public class RawMaterial
{
    [Header("Raw Material")]
    public string RawMaterialName;
    [TextArea]
    public string Description;
    public Sprite RawMaterials;

    public Sprite GetSprite()
    {
        return this.RawMaterials;
    }
    public string GetName()
    {
        return this.RawMaterialName;
    }
    public string GetDescription()
    {
        return this.Description;
    }
}

[Serializable]
public class Obstacle
{
    [Header("Obstacles")]
    public string ObstacleName;
    public string ObstaclesPath;

    public GameObject GetObstacle()
    {
        GameObject Obstacle = Resources.Load<GameObject>(ObstaclesPath);
        return Obstacle;
    }
}

[Serializable]
public class Weapon
{
    [Header("Player")]
    public string WeaponName;
    public WeapID WeaponID;
    public string WeaponPath;
    public GameObject GetWeaponPrefab()
    {
        GameObject weapon = Resources.Load<GameObject>(WeaponPath);
        return weapon;
    }
}

