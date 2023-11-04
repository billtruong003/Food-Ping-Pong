using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersist : MonoBehaviour
{
    public static DataPersist Instance;
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
}
