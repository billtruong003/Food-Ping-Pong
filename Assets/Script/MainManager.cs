using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;
using NaughtyAttributes;

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
    [SerializeField] private bool CheatOpen;
    [SerializeField] private ShakePreset shakeData;
    private ShakeInstance shakeInstance;
    private ShakeParameters shakeParameters;

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
    public void ShakeCamera()
    {
        if (shakeInstance == null)
        {
            shakeInstance = Shaker.ShakeAll(shakeData);
        }
        else
        {
            shakeInstance.Stop(shakeData.FadeOut, true);
            shakeInstance = null;
            shakeInstance = Shaker.ShakeAll(shakeData);
        }
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
        yield return new WaitUntil(() => m_currentState == GameState.PLAYER_STATE);
        m_currentState = GameState.UI_STATE;
        yield return new WaitForSeconds(1);
        UIManager.Instance.PopUpText($"Turn {LevelManager.Instance.GetGameTurn()}");
        m_currentState = GameState.PLAYER_STATE;

    }

    private void InitWeapTurn()
    {
        LevelManager.Instance.SetTurn();
    }

    private void SpawnPlayer()
    {
        StartCoroutine(Cor_SpawnPlayer());
    }
    public IEnumerator Cor_SpawnPlayer()
    {
        WeapID playerID = m_player;
        if (!CheatOpen)
        {
            yield return new WaitUntil(() => PlayerData.Instance != null);
            playerID = PlayerData.Instance.GetWeapInUse();
        }
        GameObject WeapPrefab = gameConfig.GetWeapon(playerID);
        Player = Instantiate(WeapPrefab, SpawnPos);
        Player.transform.localPosition = Vector3.zero;
        StartCoroutine(GameStart());
    }

    public void SpawnMaterial()
    {
        m_currentState = GameState.RESPAWN_STATE;
        pickUpSpawner.SpawnPickUp();
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
                UIManager.Instance.GameOver();
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
