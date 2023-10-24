using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject PickUpPrefab;
    [SerializeField] private Transform PickUpContainer;
    [SerializeField] private Vector2 LimitTopLeft;
    [SerializeField] private Vector2 LimitDownRight;
    private float positionX;
    private float positionY;

    public void SpawnPickUp(int timeSpawn)
    {
        StartCoroutine(Cor_SpawnPickUp(timeSpawn));
    }

    private IEnumerator Cor_SpawnPickUp(int numSpawn)
    {
        for (int i = 0; i < numSpawn; i++)
        {
            positionX = Random.Range(LimitTopLeft.x, LimitDownRight.x);
            positionY = Random.Range(LimitTopLeft.y, LimitDownRight.y);
            GameObject pickUp = GameManager.Instance.GetRawMaterial();
            Instantiate(pickUp, PickUpContainer.GetChild(0));
            pickUp.transform.localScale = Vector3.one;
            pickUp.transform.localPosition = new Vector3(positionX, positionY);
            LevelManager.Instance.IncreasePickUp();
            yield return new WaitForSeconds(0.5f);
        }
        GameManager.Instance.InitState(0);
    }
}
