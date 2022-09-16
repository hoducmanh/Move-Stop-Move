using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float flyingSpeed;
    [SerializeField] private float rotatingSpeed;
    [SerializeField] private float weaponExistTime;
    public Transform weaponTrans;
    public GameObject weaponPrefab;
    private Vector3 rotateDir = Vector3.up;
    private Vector3 targetPos;
    private float timer;
    void Update()
    {
        Flying();
        WeaponExistTimer();
    }
    private void Flying()
    {
        weaponTrans.position = Vector3.MoveTowards(weaponTrans.position, targetPos, flyingSpeed * Time.deltaTime);
        weaponTrans.Rotate(rotateDir * flyingSpeed * Time.deltaTime * 180, Space.World);
    }
    public void SetupWeapon(Vector3 dir)
    {
        targetPos = dir + Vector3.up * 2;
    }
    private void WeaponExistTimer()
    {
        //Debug.Log(timer);
        if(timer < weaponExistTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            ItemPooling.Instance.DespawnToPool(WeaponType.axe, weaponPrefab);
        }
    }
    private void ResetTimer()
    {
        
    }
}