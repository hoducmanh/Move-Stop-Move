using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { axe, dagger, arrow }
public enum WeaponSkinType
{
    Axe_0,
    Axe_0_2,
    Hammer_1,
    Hammer_2,
    Candy_0_1,
    Candy_0_2,
    Knife_1,
    Knife_2,
    Candy_1_1,
    Candy_1_2
}
public enum HatType
{
    None,
    Arrow,
    Cowboy,
    Crown,
    Ear,
    Hat,
    Cap,
    StrawHat,
    HeadPhone,
    Horn
}

public class ItemPooling : Singleton<ItemPooling>
{
    [System.Serializable]
    public class WeaponData
    {
        public WeaponType tag;
        public GameObject prefab;
        public int size;
    }
    public class WeaponSkinData
    {
        public WeaponSkinType WeaponSkinTag;
        public Material WeaponSkinMaterial;
    }
    [System.Serializable]
    public class HatData
    {
        public HatType hatTag;
        public GameObject hatPrefab;
        public int size;
    }
    [NonReorderable]
    public List<WeaponData> WeaponTypeDatas;
    [NonReorderable]
    public List<WeaponSkinData> WeaponSkinDatas;
    [NonReorderable]
    public List<HatData> HatDatas;

    public List<Material> BotMaterials;
    public List<string> BotNames;

    private Dictionary<WeaponType, GameObject> weaponItems = new Dictionary<WeaponType, GameObject>();
    private Dictionary<WeaponSkinType, Material> weaponSkins = new Dictionary<WeaponSkinType, Material>();
    private Dictionary<HatType, GameObject> hatItems = new Dictionary<HatType, GameObject>();
    private Dictionary<WeaponType, Queue<GameObject>> weaponPool = new Dictionary<WeaponType, Queue<GameObject>>();
    private Dictionary<HatType, Queue<GameObject>> hatPool = new Dictionary<HatType, Queue<GameObject>>();
    void Awake()
    {
        foreach (var item in WeaponTypeDatas)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < item.size; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            weaponPool.Add(item.tag, objectPool);
        }
        foreach (var item in HatDatas)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < item.size; i++)
            {
                GameObject obj = Instantiate(item.hatPrefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            hatPool.Add(item.hatTag, objectPool);
        }
    }
    public GameObject SpawnWeaponFromPool(WeaponType tag, WeaponSkinType skinTag, Vector3 position, Quaternion rotation)
    {        
        if (weaponPool[tag].Count <= 0)
        {
            foreach (WeaponData item in WeaponTypeDatas)
            {
                if (item.tag == tag)
                {
                    GameObject obj = Instantiate(item.prefab);
                    weaponPool[tag].Enqueue(obj);
                }
            }
        }
        GameObject objectToSpawn = weaponPool[tag].Dequeue();
        //weapon = CacheWeapon.Get(objectToSpawn);
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPoolWeapon poolWeapon = CacheIPoolWeapon.Get(objectToSpawn);
        poolWeapon?.OnSpawnFromPool(weaponSkins[skinTag]);
        return objectToSpawn;
    }

    public void DespawnWeaponToPool(WeaponType tag, GameObject objectToDespawn)
    {
        objectToDespawn.SetActive(false);
        IPoolWeapon poolWeapon = CacheIPoolWeapon.Get(objectToDespawn);
        poolWeapon?.OnDespawnToPool();
        weaponPool[tag].Enqueue(objectToDespawn);
    }
}

