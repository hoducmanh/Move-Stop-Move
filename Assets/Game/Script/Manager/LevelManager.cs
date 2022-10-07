using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static GameManager;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int numOfExistEnemy = 10;
    [SerializeField] private int numOfTotalEnemy = 30; 
    public Transform playerTrans;
    public float spawnRadius;
    public float outRadius;
    private List<float> spawn =  new List<float>() { -20f, -16f, 16f, 20f};
    void Awake()
    {
        SpawnBaseEnemy();
    }
    private void Start()
    {
        GameManager.OnGameStateChange += GameManagerOnGameStateChange;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= GameManagerOnGameStateChange;
    }
    private void SpawnBaseEnemy()
    {
        for(int i = 0; i < numOfExistEnemy; i++)
        {
            SpawnEnemyOnRandomPos();
        }
    }
    private void SpawnEnemyOnRandomPos()
    {
        Vector3 enemyPos = GetRandomPos();
        GetRandomPos();
        EnemyPooling.Instance.SpawnEnemyFromPool(enemyPos, Quaternion.identity);
    }
    private Vector3 GetRandomPos()
    {
        for (int i = 0; i < 150; i++)
        {
            float x, z;
            x = Random.Range(-16f, 16f);
            z = Random.Range(-16f, 16f);
            Vector3 pos = new Vector3(x, 0, z);
            float dis = (Vector3.zero - pos).sqrMagnitude;
            if (dis < spawnRadius * spawnRadius)
            {
                continue;
            }
            else
            {
                return pos;
            }
        }
        int posX, posZ;
        posX = Random.Range(0, 3);
        posZ = Random.Range(0, 3);
        return new Vector3(spawn[posX], 0, spawn[posZ]);
    }
    private void GameManagerOnGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.LoadLevel: 
                break;
            default:
                break;
        }
    }
}
