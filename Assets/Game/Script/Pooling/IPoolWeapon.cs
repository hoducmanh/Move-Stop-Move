using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolWeapon
{
    public void OnSpawnFromPool(Material skinMaterial);
    public void OnDespawnToPool();
}
