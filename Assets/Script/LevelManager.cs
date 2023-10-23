using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private int pickUpLeft;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncreasePickUp()
    {
        pickUpLeft++;
    }

    public void DecreasePickUp()
    {
        pickUpLeft--;
    }
    public void CheckPickUpLeft()
    {
        if (pickUpLeft == 0)
        {

        }
    }
}
