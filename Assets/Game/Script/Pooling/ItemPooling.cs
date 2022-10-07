using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { axe, candy, dagger, hammer}
public enum WeaponSkinType
{
    Axe_0,
    Axe_0_2,
    Hammer_1,
    Hammer_2,
    Candy_0_1,
    Candy_0_2,
    Knife_1,
    Knife_2
}
public enum HatType
{
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
    public class WeaponTypeData
    {
        public WeaponType WeaponTag;
        public Weapon WeaponPrefab;
        public int PoolSize;
    }
    [System.Serializable]
    public class WeaponSkinData
    {
        public WeaponSkinType WeaponSkinTag;
        public Material WeaponSkinMaterial;
    }
    [System.Serializable]
    public class HatData
    {
        public HatType HatTag;
        public GameObject HatPrefabs;
        public int poolSize;
    }
    private List<WeaponTypeData> weaponTypeDatas;
    private List<WeaponSkinData> weaponSkinDatas;
    private List<HatData> hatDatas;
    private List<Material> botMaterials;
    private List<string> botNames;

    public WeaponDataSO WeaponDataSO;
    public HatDataSO HatDataSO;
    public BotDataSO BotDataSO;

    private Dictionary<WeaponType, Weapon> weaponItems = new Dictionary<WeaponType, Weapon>();
    private Dictionary<WeaponSkinType, Material> weaponSkins = new Dictionary<WeaponSkinType, Material>();
    private Dictionary<HatType, GameObject> hatItems = new Dictionary<HatType, GameObject>();

    private Dictionary<WeaponType, Stack<Weapon>> weaponPool = new Dictionary<WeaponType, Stack<Weapon>>();
    private Dictionary<HatType, Stack<GameObject>> hatPool = new Dictionary<HatType, Stack<GameObject>>();

    void Awake()
    {
        InputScriptableObjectData();
        foreach (var item in weaponTypeDatas)
        {
            weaponItems.Add(item.WeaponTag, item.WeaponPrefab);
        }
        foreach (var item in weaponSkinDatas)
        {
            weaponSkins.Add(item.WeaponSkinTag, item.WeaponSkinMaterial);
        }
        foreach (var item in hatDatas)
        {
            hatItems.Add(item.HatTag, item.HatPrefabs);
        }
        foreach (var item in weaponTypeDatas)
        {
            Stack<Weapon> tmpStack = new Stack<Weapon>();
            for (int i = 0; i < item.PoolSize; i++)
            {
                Weapon tmpObj = Instantiate(item.WeaponPrefab);
                tmpStack.Push(tmpObj);
                tmpObj.weaponObj.SetActive(false);
            }

            weaponPool.Add(item.WeaponTag, tmpStack);
        }
        foreach (var item in hatDatas)
        {
            Stack<GameObject> tmpStack = new Stack<GameObject>();
            for (int i = 0; i < item.poolSize; i++)
            {
                GameObject tmpObj = Instantiate(item.HatPrefabs);
                tmpStack.Push(tmpObj);

                tmpObj.SetActive(false);
            }

            hatPool.Add(item.HatTag, tmpStack);
        }
    }
    public T SpawnWeaponFromPool<T>(WeaponType tag, WeaponSkinType skinTag, Vector3 position, Quaternion rotation) where T : Weapon
    {
        Weapon weapon; 
        if (weaponPool[tag].Count <= 0)
        {
            weapon = Instantiate(weaponItems[tag]);
        }
        else
        {
            weapon = weaponPool[tag].Pop();
        }
        GameObject obj = weapon.weaponObj;
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        IPoolWeapon poolWeapon = CacheIPoolWeapon.Get(weapon);
        poolWeapon?.OnSpawnFromPool(weaponSkins[skinTag]);
        return weapon as T;
    }

    public void DespawnWeaponToPool(Weapon objectToDespawn)
    {
        weaponPool[objectToDespawn.weaponType].Push(objectToDespawn);
        IPoolWeapon poolWeapon = CacheIPoolWeapon.Get(objectToDespawn);
        poolWeapon?.OnDespawnToPool();
        objectToDespawn.weaponObj.SetActive(false);
    }
    private void InputScriptableObjectData()
    {
        weaponTypeDatas = WeaponDataSO.WeaponDatas;
        weaponSkinDatas = WeaponDataSO.WeaponSkinDatas;
        hatDatas = HatDataSO.HatDatas;
        botMaterials = BotDataSO.BotMaterials;
        botNames = BotDataSO.BotNames;
    }
    public Material GetRandomMaterial()
    {
        int randomNum = Random.Range(0, botMaterials.Count);
        return botMaterials[randomNum];
    }
    public string GetRandomName()
    {
        int randomNum = Random.Range(0, botNames.Count);
        return botNames[randomNum];
    }
    public Material GetWeaponSkin(WeaponSkinType tag)
    {
        return weaponSkins[tag];
    }
}

