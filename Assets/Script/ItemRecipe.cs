using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ItemRecipe : MonoBehaviour
{
    [SerializeField] private Image recipeImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Color activeCol;
    [SerializeField] private Color inActiveCol;
    [SerializeField] private string count;
    [SerializeField] private string recipeName;
    public void SetUpRecipes(Sprite icon, string name, string countText, bool activate = false)
    {
        this.recipeName = name;
        this.count = countText;

        this.recipeImage.sprite = icon;
        this.nameText.text = recipeName;
        this.countText.text = count;
        if (activate != true)
        {
            recipeImage.color = inActiveCol;
            return;
        }
        recipeImage.color = activeCol;
    }
}
