using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType {Player}
public class EnemyPooling : Singleton<EnemyPooling>
{
    [System.Serializable]
    public class Pool
    {
        public GameObject enemyPrefab;
        public int size;
    }
    [NonReorderable]
    public Pool pools;
    public Stack<GameObject> enemyPool = new Stack<GameObject>();
    protected void Awake()
    {
        for (int i = 0; i < pools.size; i++)
        {
            GameObject obj = Instantiate(pools.enemyPrefab);
            obj.SetActive(false);
            enemyPool.Push(obj);
        }
    }
    public GameObject SpawnEnemyFromPool(Vector3 position, Quaternion rotation)
    {
        GameObject obj;
        if (enemyPool.Count <= 0)
        {
            obj = Instantiate(pools.enemyPrefab);
        }
        else
        {
            obj = enemyPool.Pop();
        }
        Transform objTrans = obj.transform;

        obj.SetActive(true);
        objTrans.position = position;
        objTrans.rotation = rotation;

        IPoolCharacter poolCharacter = CacheIPoolCharacter.Get(obj);
        poolCharacter?.OnSpawn();

        return obj;
    }

    public void DespawnEnemyToPool(GameObject obj)
    {
        if (!enemyPool.Contains(obj))
        {
            IPoolCharacter poolCharacter = CacheIPoolCharacter.Get(obj);
            poolCharacter?.OnDespawn();
            obj.SetActive(false);
            enemyPool.Push(obj);
        }
    }

}
