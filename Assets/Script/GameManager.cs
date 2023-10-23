using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PickUpConfig GameConfig;
    [SerializeField] private string m_player;
    [SerializeField] private Transform SpawnPos;
    [SerializeField] private GameState m_currentState;

    public enum GameState
    {
        NONE,
        UI_STATE,
        PLAYER_STATE,
        RESPAWN_STATE,
    }
    private void Awake()
    {
        if (Instance == null)
        {
            // Nếu chưa có instance, đặt instance thành this
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Nếu đã có instance khác tồn tại, hủy bỏ đối tượng này
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SpawnPlayer();
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

}
