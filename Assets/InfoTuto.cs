using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InfoTuto : MonoBehaviour
{
    [SerializeField] private Image img;
    [SerializeField] private TextMeshProUGUI step;
    [SerializeField] private TextMeshProUGUI info;
    public void SetData(Sprite sprite, string step, string info)
    {
        img.sprite = sprite;
        this.step.text = step;
        this.info.text = info;
    }
}
