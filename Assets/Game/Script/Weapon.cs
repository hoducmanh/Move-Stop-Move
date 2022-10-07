using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float flyingSpeed;
    [SerializeField] private float rotatingSpeed;
    [SerializeField] private float weaponExistTime;
    [SerializeField] private Vector3 weaponPositionOffset;
    private Quaternion weaponOnHandRotationOffset;
    private Quaternion weaponThrowRotationOffset;
    public Vector3 handRotationOffset;
    public Vector3 throwRotationOffset;
    public Renderer weaponRenderer;
    public WeaponType weaponTag;
    public Transform weaponTrans;
    public GameObject weaponObj;
    public WeaponType weaponType;
    public Collider weaponCol;
    public float weaponScale;
    private Vector3 rotateDir = Vector3.up;
    private Vector3 targetPos;
    private Character weaponOwner;
    private float timer;
    protected virtual void Awake()
    {
        weaponOnHandRotationOffset = Quaternion.Euler(handRotationOffset.x, handRotationOffset.y, handRotationOffset.z);
        weaponThrowRotationOffset = Quaternion.Euler(throwRotationOffset.x, throwRotationOffset.y, throwRotationOffset.z);
    }
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
    public void SetupOnHandWeapon(Character owner)
    {
        this.enabled = false;
        weaponCol.enabled = false;
        weaponTrans.localScale = weaponScale * Vector3.one;

        weaponTrans.localPosition = weaponPositionOffset;
        weaponTrans.localRotation = weaponOnHandRotationOffset;

        owner?.SetupWeaponRotation(weaponThrowRotationOffset);
    }
    private void WeaponExistTimer()
    {
        if(timer < weaponExistTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            ItemPooling.Instance.DespawnWeaponToPool(this);
            timer = 0;
        }
    }
    public void DespawnWeaponToPool()
    {
        this.enabled = true;
        weaponCol.enabled = true;
    }
    public void SetupWeapon(Vector3 dir, Character owner)
    {
        SetTargetDir(dir);
        SetOwner(owner);
        SetSize(owner);
        OnThrowHandle();
    }
    public void SetTargetDir(Vector3 dir)
    {
        targetPos = dir;
    }
    public void SetOwner(Character owner)
    {
        weaponOwner = owner;
    }
    public void SetSize(Character owner)
    {
        weaponTrans.localScale = weaponOwner.meshPlayer.localScale * weaponScale;
        flyingSpeed = 8f * weaponOwner.meshPlayer.localScale.x;
    }
    protected virtual void OnThrowHandle()
    {
    }
    public void SpawnWeaponFromPool(WeaponSkinType weaponSkinTag)
    {
        Material weaponSkinMaterial = ItemPooling.Instance.GetWeaponSkin(weaponSkinTag);
        switch (weaponTag)
        {
            case WeaponType.candy:
                weaponRenderer.materials = new Material[] { weaponSkinMaterial, weaponSkinMaterial, weaponSkinMaterial };
                break;
            default:
                weaponRenderer.materials = new Material[] { weaponSkinMaterial, weaponSkinMaterial };
                break;
        }
        timer = 0;
    }
}
