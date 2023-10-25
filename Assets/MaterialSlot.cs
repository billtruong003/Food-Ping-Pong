using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialSlot : MonoBehaviour
{
    [SerializeField] private string materialName;
    [SerializeField] private Image materialSprite;
    public string MatName => materialName;

    [Header("Count Material")]
    [SerializeField] private TextMeshProUGUI countNumb;
    [SerializeField] private int materialCount = 0;

    public void MaterialSet(string name)
    {
        materialName = name;
    }
    public void MaterialTrigger(string name)
    {
        MaterialSet(name);

        materialCount = 0;
        MaterialAdded();

        Sprite spriteMat = GameManager.Instance.GetSpriteMaterial(name);
        materialSprite.sprite = spriteMat;

        materialSprite.gameObject.SetActive(true);
        countNumb.gameObject.SetActive(true);
    }
    public void MaterialAdded()
    {
        materialCount++;
        countNumb.text = materialCount.ToString();
        LevelManager.Instance.InventoryAdd(materialName);
    }
    public void MaterialDecreased()
    {
        materialCount--;
        countNumb.text = materialCount.ToString();
        if (materialCount == 0)
        {
            materialSprite.gameObject.SetActive(false);
            countNumb.gameObject.SetActive(false);
            transform.SetAsLastSibling();
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
