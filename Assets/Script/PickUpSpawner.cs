using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private Transform PickUpContainer;
    [SerializeField] private Vector2 LimitTopLeft;
    [SerializeField] private Vector2 LimitDownRight;
    [SerializeField] private bool CheatSpawn;
    [SerializeField] private int NumberSpawnCheat;
    [SerializeField] private List<string> lstStringMaterials;
    private float positionX;
    private float positionY;

    public void SpawnPickUp()
    {
        int timeSpawn = NumberSpawnCase();
        lstStringMaterials = MainManager.Instance.GetListMaterialToSpawn();
        if (CheatSpawn)
            timeSpawn = NumberSpawnCheat;
        for (int i = 0; i < timeSpawn; i++)
        {
            StartCoroutine(Cor_SpawnPickUp(timeSpawn));
        }
    }
    private int NumberSpawnCase()
    {
        int numCase = LevelManager.Instance.GetGameTurn();
        if (numCase > 0 && numCase <= 20)
        {
            return 4;
        }
        else if (numCase > 20 && numCase <= 40)
        {
            return 5;
        }
        else if (numCase > 40 && numCase <= 60)
        {
            return 6;
        }
        else if (numCase > 60 && numCase <= 80)
        {
            return 7;
        }
        else if (numCase > 80 && numCase <= 100)
        {
            return 8;
        }
        return 8;
    }

    private IEnumerator Cor_SpawnPickUp(int numSpawn)
    {
        yield return new WaitUntil(() => MainManager.Instance != null);
        SoundManager.Instance.PlaySFX(4);
        positionX = Random.Range(LimitTopLeft.x, LimitDownRight.x);
        positionY = Random.Range(LimitTopLeft.y, LimitDownRight.y);
        GameObject pickUp = MainManager.Instance.GetRawMaterial(lstStringMaterials);
        Instantiate(pickUp, PickUpContainer.GetChild(0));
        pickUp.transform.localScale = Vector3.one;
        pickUp.transform.localPosition = new Vector3(positionX, positionY);
        yield return new WaitForSeconds(0.5f);

        MainManager.Instance.InitState(0);
    }
}
