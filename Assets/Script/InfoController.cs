using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InfoController : MonoBehaviour
{
    [SerializeField] private Image item;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI descriptionMaterial;
    public void UpdateInfo(Sprite sprite, string matName, string description)
    {
        item.sprite = sprite;
        itemName.text = matName;
        descriptionMaterial.text = description;
    }
}
