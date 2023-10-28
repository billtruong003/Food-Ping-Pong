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
    [SerializeField] private PickUpConfig gameConfig;
    [SerializeField] private WeapID m_player;
    [SerializeField] private Transform SpawnPos;
    [SerializeField] private int timeSpawn;


    public enum GameState
    {
        NONE,
        PLAYER_STATE,
        RESPAWN_STATE,
        GAME_OVER,

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
        LevelManager.Instance.ClearInventory();
        SpawnPlayer();
        SpawnMaterial();
        InitWeapTurn();
    }

    private IEnumerator GameStart()
    {
        yield return null;
    }
    private void InitWeapTurn()
    {
        LevelManager.Instance.SetTurn();
    }
    private void SpawnPlayer()
    {
        GameObject WeapPrefab = gameConfig.GetWeapon(m_player);
        GameObject Weap = Instantiate(WeapPrefab, SpawnPos);
        Weap.transform.localPosition = Vector3.zero;
    }

    public void SpawnMaterial()
    {
        m_currentState = GameState.RESPAWN_STATE;
        timeSpawn = Random.Range(2, 5);
        pickUpSpawner.SpawnPickUp(timeSpawn);
    }
    public GameObject GetRawMaterial()
    {
        int rawMatNum = Random.Range(0, gameConfig.RawMaterials.Count);
        GameObject rawMat = gameConfig.GetPickUpMaterial(rawMatNum);
        return rawMat;
    }
    public Sprite GetSpriteMaterial(string name)
    {
        return gameConfig.GetSpriteMat(name);
    }
    public string GetDescriptionMaterial(string name)
    {
        return gameConfig.GetMatDescription(name);
    }
    public Meal GetMeal()
    {
        return gameConfig.GetRandomMeal();
    }

    public void InitState(int id)
    {
        switch (id)
        {
            case (0):
                m_currentState = GameState.PLAYER_STATE;
                break;
            case (1):
                m_currentState = GameState.RESPAWN_STATE;
                break;
            case (2):
                m_currentState = GameState.GAME_OVER;
                break;
        }

    }


}
