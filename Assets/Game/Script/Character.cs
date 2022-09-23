using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject tempPlayer;
    public Transform meshPlayer;
    public Animator _animator;
    public Transform PlayerTrans;
    public GameObject WeaponHolder;
    public bool isDead;
    public float speed;
    protected string currAnim = Value.CURRENT_ANIM_IDLE;
    protected List<Character> targetPosition = new List<Character>();
    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("add");
        if (other.CompareTag(Value.PLAYER))
        {
            
            targetPosition.Add(other.GetComponent<Character>());
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
    public void Dead()
    {
        if (isDead)
        {
            ChangeAnimation(Value.CURRENT_ANIM_DEAD);
        }
    }
    public void Throwing(Vector3 target)
    {
        
        Weapon weaponToThrow = ItemPooling.Instance.SpawnFromPool(WeaponType.axe, WeaponHolder.transform.position, Quaternion.identity).GetComponent<Weapon>();
        Debug.Log(weaponToThrow);
        weaponToThrow.SetRotation(); 
        weaponToThrow.SetupWeapon(target);
    }
}
