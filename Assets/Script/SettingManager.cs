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
    [SerializeField] private TextMeshProUGUI musicValue, soundValue;
    // [SerializeField] private Slider musicVolume, soundVolume;
    public void ShowMusic (float value)
    {
        musicValue.text = value.ToString() + "%";
        SoundManager.Instance.SetMusicVol(value);
    }
    public void ShowSound (float value)
    {
        soundValue.text = value.ToString() + "%";
        SoundManager.Instance.SetSoundVol(value);
    }
    // public void ChangeMusicVol (float value)
    // {
    //     SoundManager.Instance.SetMusicVol(value);
    // }
}
