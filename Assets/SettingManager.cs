using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SettingManager : MonoBehaviour
{
    // public static SettingManager Instance;
    // private void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }
    [SerializeField] private TextMeshProUGUI soundVolume;
    [SerializeField] private Slider sliderVolume;
    public void ShowNumber (float value)
    {
        soundVolume.text = value.ToString() + "%";
        // sliderVolume.onValueChanged.AddListener(val => SoundManager.Instance.SetVolume(val));
    }
    // public void AcceptQuality (int index)
    // {
    //     // QualitySettings.SetQualityLevel(index);
    // }
    public void ChangeVolume (float volume)
    {
        SoundManager.Instance.SetVolume(volume);
    }
}
