using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType {Player}
public class EnemyPooling : Singleton<EnemyPooling>
{
    [System.Serializable]
    public class Pool
    {
        public EnemyType tag;
        public GameObject prefab;
        public int size;
    }
    [NonReorderable]
    public List<Pool> pools;
    public Dictionary<EnemyType, Queue<GameObject>> poolDictionary;
    void Awake()
    {
        poolDictionary = new Dictionary<EnemyType, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject SpawnEnemyFromPool(EnemyType tag, Vector3 position, Quaternion rotation)
    {

        if (poolDictionary[tag].Count <= 0)
        {
            foreach (Pool pool in pools)
            {
                if (pool.tag == tag)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    poolDictionary[tag].Enqueue(obj);
                }
            }
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        return objectToSpawn;
    }

    public void DespawnEnemyToPool(EnemyType tag, GameObject prefab)
    {
        prefab.SetActive(false);
        poolDictionary[tag].Enqueue(prefab);
    }

}
