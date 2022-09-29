using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int numOfExistEnemy = 10;
    [SerializeField] private int numOfTotalEnemy = 30; 
    public Transform playerTrans;
    public float spawnRadius;
    private List<int> spawn =  new List<int>() { -20, -16, 16, 20};
    void Start()
    {
        SpawnBaseEnemy();
    }

    void Update()
    {
        
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
        Vector3 enemyPos = GetRandomDir();
        EnemyPooling.Instance.SpawnEnemyFromPool(EnemyType.Player, enemyPos, Quaternion.identity);
    }
    private Vector3 GetRandomDir()
    {
        for(int i = 0; i < 20; i++)
        {
            float x, z;
            x = Random.Range(-30f, 30f);
            z = Random.Range(-30f, 30f);
            Vector3 pos = new Vector3(x, 0, z);
            float dis = (playerTrans.localPosition - pos).sqrMagnitude;
            if (dis < spawnRadius)
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
}
