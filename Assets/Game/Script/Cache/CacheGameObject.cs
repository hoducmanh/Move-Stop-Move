using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheGameObject<T>
{
    public static Dictionary<GameObject, T> dict = new Dictionary<GameObject, T>();

    public static T Get(GameObject key)
    {
        if (!dict.ContainsKey(key))
        {
            dict.Add(key, key.GetComponent<T>());
        }

        return dict[key];
    }
}
