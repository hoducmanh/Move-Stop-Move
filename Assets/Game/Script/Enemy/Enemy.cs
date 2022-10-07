using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : Character, IPoolCharacter
{
    public AIAgent agent;
    public Transform trans;
    public Collider Col;
    public Collider sphereCol;
    public bool isAttackable = false;
    private Quaternion curRotation;
    public void OnSpawn()
    {
        isDead = false;
        EquipWeapon();
        SetEquipment();
        SetRandomName();
        SetRandomBodySkin();
    }
    public void OnDespawn()
    {

    }
    public bool ScanningEnemy()
    {
        if (targetPosition.Count > 0)
        {
            if (targetPosition[0].isDead == false)
            {
                return true;
            }
                
            else
            {
                targetPosition.RemoveAt(0);
                return false;
            }
        }
        else return false;
    }
    public void EnemyAttack()
    {
        sphereCol.enabled = false;
        Vector3 currTarget = Vector3.zero;
        currTarget = (targetPosition[0].playerTrans.position - meshPlayer.localPosition);
        trans.LookAt(currTarget);
        curRotation = Quaternion.LookRotation(currTarget);
        Throwing(currTarget);
    }
    private void SetEquipment()
    {
        weaponTag = (WeaponType)Random.Range((int)WeaponType.hammer, (int)WeaponType.candy + 1);
        switch (weaponTag)
        {
            case WeaponType.axe:
                weaponSkinTag = (WeaponSkinType)Random.Range((int)WeaponSkinType.Axe_0, (int)WeaponSkinType.Axe_0_2 + 1);
                break;
            case WeaponType.dagger:
                weaponSkinTag = (WeaponSkinType)Random.Range((int)WeaponSkinType.Knife_1, (int)WeaponSkinType.Knife_2 + 1);
                break;
            case WeaponType.candy:
                weaponSkinTag = (WeaponSkinType)Random.Range((int)WeaponSkinType.Candy_0_1, (int)WeaponSkinType.Candy_0_2 + 1);
                break;
            case WeaponType.hammer:
                weaponSkinTag = (WeaponSkinType)Random.Range((int)WeaponSkinType.Hammer_1, (int)WeaponSkinType.Hammer_2 + 1);
                break;
            default:
                break;
        }
        hatTag = (HatType)Random.Range((int)HatType.Arrow, (int)HatType.Horn + 1);

    }
    public void SetRandomBodySkin()
    {
        playerRenderer.material = ItemPooling.Instance.GetRandomMaterial();
    }
    public void SetRandomName()
    {
        playerName = ItemPooling.Instance.GetRandomName();
    }
}
