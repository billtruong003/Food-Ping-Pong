using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private TutorialConfig tutoConfig;
    [SerializeField] private Transform containerScrolling;

    public void Awake()
    {
        // InitTutoData();
    }
    [Button]
    public void InitTutoData()
    {
        List<TutoInfo> tutoInfos = tutoConfig.tutoInfos;
        for (int i = 0; i < containerScrolling.childCount; i++)
        {
            int index = tutoInfos.Count - (1 + i);
            InfoTuto infoSet = containerScrolling.GetChild(i).GetComponent<InfoTuto>();
            infoSet.SetData(tutoInfos[index].GetSprite(), tutoInfos[index].GetStep(), tutoInfos[index].GetInfo());
        }
    }
    public void CheckNewPlayer()
    {
        PlayerData.Instance.NewPlay = false;
        PlayerData.Instance.PrefSaveNewPlay();
        GetComponent<HandleScene>().InitScene("Gameplay");
    }
}
