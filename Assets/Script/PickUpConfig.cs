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
    private const string pickUpPath = "Pickup";
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
            weapon.WeaponPath = WeapPath + weapon.WeaponName;
        }
    }

    public int GetRandomPickUp()
    {
        int rndPickUp = UnityEngine.Random.Range(0, RawMaterials.Count);
        return rndPickUp;
    }

    public GameObject GetWeapon(string weaponName)
    {
        foreach (var weap in Weapons)
        {
            if (weaponName == weap.WeaponName)
            {
                return weap.GetWeaponPrefab();
            }
        }
        return null;
    }

    [Serializable]
    public class Meal
    {
        [Header("Food")]
        public string Food;
        public string FoodSpritePath;
        [Header("Infomation")]
        [TextArea]
        public string Descriptions;

        [Header("Raw Material")]
        public List<string> RawMaterialName;
        public int ItemContains = 0;
        public string NoticeInfo = "";
        public Sprite GetFoodIcon()
        {
            Sprite MealSprite = Resources.Load<Sprite>(FoodSpritePath);
            return MealSprite;
        }

        public void CheckRawMaterial(List<string> InventoryMaterial)
        {
            foreach (string item in RawMaterialName)
            {
                if (InventoryMaterial.Contains(item))
                {
                    ItemContains += 1;
                }
                NoticeInfo = "Material: {ItemContaines}/{RawMaterialName.Count}";
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
        public string WeaponPath;
        public GameObject GetWeaponPrefab()
        {
            GameObject weapon = Resources.Load<GameObject>(WeaponPath);
            return weapon;
        }
    }
}
