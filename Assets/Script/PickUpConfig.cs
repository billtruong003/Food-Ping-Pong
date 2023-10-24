using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "MealConfig", menuName = "Food/Meals")]
public class PickUpConfig : ScriptableObject
{
    [SerializeField] public GameObject PickUpContainer;
    [SerializeField] public GameObject PickUpPrefab;
    [SerializeField] public List<Meal> DishesInformation;
    [SerializeField] public List<RawMaterial> RawMaterials;
    [SerializeField] public List<Obstacle> Obstacles;
    [SerializeField] public List<Weapon> Weapons;
    private const string WeapPath = "Weapon/";
    private const string MatPath = "RawMaterial/";
    private const string Food = "Food/";

    public GameObject GetPickUpMaterial(int index)
    {
        PickUpItem pickUpName = PickUpPrefab.GetComponent<PickUpItem>();
        pickUpName.SetMatSprite(RawMaterials[index].GetSprite());
        pickUpName.SetMatName(RawMaterials[index].GetName());

        return pickUpName.gameObject;
    }

    [Button]
    private void FillWeapPath()
    {
        foreach (var weapon in Weapons)
        {
            weapon.WeaponPath = WeapPath + weapon.WeaponName;
        }
    }

    public GameObject InitPickUpPrefab(int index)
    {
        GameObject pickUpPrefab = Instantiate(PickUpPrefab);
        SpriteRenderer pickUpSprite = pickUpPrefab.GetComponent<SpriteRenderer>();
        pickUpSprite.sprite = RawMaterials[index].GetSprite();
        return pickUpPrefab;
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
        public Sprite RawMaterials;

        public Sprite GetSprite()
        {
            return RawMaterials;
        }
        public string GetName()
        {
            return RawMaterialName;
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
