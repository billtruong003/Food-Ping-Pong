using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "TutoConfig", menuName = "Tuto/config")]
public class TutorialConfig : ScriptableObject
{
    private const string PATH_TUTO = "Pack";
    [TextArea]
    [SerializeField] private List<string> noticeInfo;
    [SerializeField] public List<TutoInfo> tutoInfos;
    [Button]
    public void FillInfo()
    {
        tutoInfos = new List<TutoInfo>();

        for (int i = 0; i < 11; i++)
        {
            TutoInfo info = new TutoInfo();
            info.pathImage = $"{PATH_TUTO}/{i + 1}";
            info.step = $"{i + 1}";
            info.info = noticeInfo[i];
            tutoInfos.Add(info);
        }
    }

}
[Serializable]
public class TutoInfo
{
    public string step;
    public string pathImage;
    public string info;
    public Sprite GetSprite()
    {
        return Resources.Load<Sprite>(pathImage);
    }
    public string GetStep()
    {
        return step;
    }
    public string GetInfo()
    {
        return info;
    }
}