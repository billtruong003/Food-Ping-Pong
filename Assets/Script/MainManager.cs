using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public GameState m_currentState;
    [SerializeField] PickUpSpawner pickUpSpawner;
    [SerializeField] private PickUpConfig gameConfig;
    [SerializeField] private WeapID m_player;
    [SerializeField] private Transform SpawnPos;
    [SerializeField] private int timeSpawn;
    [SerializeField] public GameObject Player;
    private GameState lastGameState;
    private bool gameOver = false;


    public enum GameState
    {
        NONE,
        PLAYER_STATE,
        PLAYER_SHOOTING,
        RESPAWN_STATE,
        UI_STATE,
        GAME_OVER,
    }
    public bool GetGameOver()
    {
        return gameOver;
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

    private Sprite GetSpritePlayer()
    {
        return Player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }

    private IEnumerator GameStart()
    {
        yield return new WaitUntil(() => Player != null);
        UIManager.Instance.InitWeaponInfo(GetSpritePlayer());
        yield return null;
    }

    private void InitWeapTurn()
    {
        LevelManager.Instance.SetTurn();
    }

    private void SpawnPlayer()
    {
        GameObject WeapPrefab = gameConfig.GetWeapon(m_player);
        Player = Instantiate(WeapPrefab, SpawnPos);
        Player.transform.localPosition = Vector3.zero;
        StartCoroutine(GameStart());
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

    public void changeState(int id)
    {
        lastGameState = m_currentState;
        if (lastGameState != GameState.PLAYER_STATE)
            lastGameState = GameState.PLAYER_STATE;
        InitState(id);
    }

    public void remakeState()
    {
        m_currentState = lastGameState;
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
                gameOver = true;
                break;
            case (3):
                m_currentState = GameState.UI_STATE;
                break;
            case (4):
                m_currentState = GameState.PLAYER_SHOOTING;
                break;
        }

    }


}
