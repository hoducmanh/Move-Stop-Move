using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { axe, dagger, arrow }
public class ItemPooling : Singleton<ItemPooling>
{
    [System.Serializable]
    public class Pool
    {
        public WeaponType tag;
        public GameObject prefab;
        public int size;
        public Pool(WeaponType tag, GameObject prefab, int size)
        {
            this.tag = tag;
            this.prefab = prefab;
            this.size = size;
        }
    }
    [NonReorderable]
    public List<Pool> pools;
    public Dictionary<WeaponType, Queue<GameObject>> poolDictionary;
    void Awake()
    {
        poolDictionary = new Dictionary<WeaponType, Queue<GameObject>>();
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
    public GameObject SpawnFromPool(WeaponType tag, Vector3 position, Quaternion rotation)
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

    public void DespawnToPool(WeaponType tag, GameObject prefab)
    {
        prefab.SetActive(false);
        poolDictionary[tag].Enqueue(prefab);
    }
    
}
