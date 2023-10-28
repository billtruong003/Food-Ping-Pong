using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoodDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI foodName;
    [SerializeField] private TextMeshProUGUI foodDescription;
    public void DescriptionUpdate(string name, string description)
    {
        foodName.text = name;
        foodDescription.text = description;
        gameObject.SetActive(true);
    }
}
