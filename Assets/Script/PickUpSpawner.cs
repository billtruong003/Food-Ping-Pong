using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private Transform PickUpContainer;
    [SerializeField] private Vector2 LimitTopLeft;
    [SerializeField] private Vector2 LimitDownRight;
    [SerializeField] private bool CheatSpawn;
    [SerializeField] private int NumberSpawnCheat;
    private float positionX;
    private float positionY;

    public void SpawnPickUp(int timeSpawn)
    {
        if (CheatSpawn)
            timeSpawn = NumberSpawnCheat;
        for (int i = 0; i < timeSpawn; i++)
        {
            StartCoroutine(Cor_SpawnPickUp(timeSpawn));
        }
    }

    private IEnumerator Cor_SpawnPickUp(int numSpawn)
    {
        yield return new WaitUntil(() => MainManager.Instance != null);

        positionX = Random.Range(LimitTopLeft.x, LimitDownRight.x);
        positionY = Random.Range(LimitTopLeft.y, LimitDownRight.y);
        GameObject pickUp = MainManager.Instance.GetRawMaterial();
        Instantiate(pickUp, PickUpContainer.GetChild(0));
        pickUp.transform.localScale = Vector3.one;
        pickUp.transform.localPosition = new Vector3(positionX, positionY);
        yield return new WaitForSeconds(0.5f);

        MainManager.Instance.InitState(0);
    }
}
