using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public WeaponType weaponTag { get; protected set; }
    public WeaponSkinType weaponSkinTag { get; protected set; }
    public HatType hatTag { get; protected set; }
    public Quaternion throwWeaponRotation { get; protected set; }
    public NavMeshAgent navAgent;
    public Character player;
    public GameObject tempPlayer;
    public Transform meshPlayer;
    public Animator _animator;
    public Transform playerTrans;
    public GameObject throwPos;
    public GameObject weaponHolder;
    public GameObject hatHolder;
    public Renderer playerRenderer;
    public string playerName;
    public Weapon currWeapon;
    public bool isDead;
    public float speed;
    protected string currAnim = Value.CURRENT_ANIM_IDLE;
    protected List<Character> targetPosition = new List<Character>();
    protected virtual void Awake()
    {
        throwWeaponRotation = Quaternion.Euler(-90f, 0, 90f);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Value.PLAYER))
        {
            if(other.gameObject != gameObject) 
                targetPosition.Add(other.GetComponent<Character>());
        }
        if (other.CompareTag(Value.WEAPON))
        {
            CollideWithWeapon(other);
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Value.PLAYER))
        {
            targetPosition.Remove(other.GetComponent<Character>());
        }
    }
    public void ChangeAnimation(string tempAnim)
    {
        if (currAnim != tempAnim)
        {
            _animator.SetTrigger(tempAnim);
            currAnim = tempAnim;
        }
    }
    public void EquipWeapon()
    {
        if (currWeapon != null)
        {
            currWeapon.transform.SetParent(null, false);
            ItemPooling.Instance.DespawnWeaponToPool(currWeapon);
        }
        Weapon weapon = ItemPooling.Instance.SpawnWeaponFromPool<Weapon>(weaponTag, weaponSkinTag, Vector3.zero, Quaternion.identity);
        weapon.weaponTrans.SetParent(weaponHolder.transform, false);
        currWeapon = weapon;
        weapon.SetupOnHandWeapon(this);
    }
    public void SetupWeaponRotation(Quaternion rotation)
    {
        throwWeaponRotation = rotation;
    }
    public void SetupWeaponTag(WeaponType tag)
    {
        weaponTag = tag;
    }
    public void SetWeaponSkin(WeaponSkinType tag)
    {
        weaponSkinTag = tag;
    }
    public void SetHat(HatType tag)
    {
        hatTag = tag;
    }
    public void Throwing(Vector3 targetPos)
    {
        Vector3 moveDir = targetPos;
        Weapon weaponToThrow = ItemPooling.Instance.SpawnWeaponFromPool<Weapon>(weaponTag, weaponSkinTag, throwPos.transform.position, Quaternion.identity);
        weaponToThrow.SetupWeapon(moveDir, this);
    }
    public void CollideWithWeapon(Collider other)
    {
        if (other.CompareTag(Value.WEAPON))
        {
            ChangeAnimation(Value.CURRENT_ANIM_DEAD);
            Dead();
            player.enabled = false;
            navAgent.enabled = false;
            isDead = true;
        }
    }
    public void Dead()
    {
        if (isDead)
        {
            ChangeAnimation(Value.CURRENT_ANIM_DEAD);
        }
    }
}
