using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState m_currentState;
    [SerializeField] PickUpSpawner pickUpSpawner;
    [SerializeField] private PickUpConfig GameConfig;
    [SerializeField] private string m_player;
    [SerializeField] private Transform SpawnPos;
    [SerializeField] private int timeSpawn;


    public enum GameState
    {
        NONE,
        UI_STATE,
        PLAYER_STATE,
        RESPAWN_STATE,
        END_STATE,
    }

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

    void Start()
    {
        SpawnPlayer();
        timeSpawn = Random.Range(2, 5);
        SpawnMaterial();
    }

    private IEnumerator GameStart()
    {
        yield return null;
    }

    private void SpawnPlayer()
    {
        GameObject WeapPrefab = GameConfig.GetWeapon(m_player);
        GameObject Weap = Instantiate(WeapPrefab, SpawnPos);
        Weap.transform.localPosition = Vector3.zero;
    }

    public void SpawnMaterial()
    {
        m_currentState = GameState.RESPAWN_STATE;
        pickUpSpawner.SpawnPickUp(timeSpawn);
    }

    public void InitState(int id)
    {
        switch (id)
        {
            case (0):
                m_currentState = GameState.PLAYER_STATE;
                break;
            case (1):
                m_currentState = GameState.UI_STATE;
                break;
            case (2):
                m_currentState = GameState.RESPAWN_STATE;
                break;
            case (3):
                m_currentState = GameState.END_STATE;
                break;
        }

    }


}
