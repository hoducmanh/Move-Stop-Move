using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject tempPlayer;
    public Transform meshPlayer;
    public Animator _animator;
    public Transform PlayerTrans;
    public Transform WeaponHolder;

    //public Weapon ;
    public bool isDead;
    public float speed;
    protected string currAnim = Value.CURRENT_ANIM_IDLE;
    protected List<Transform> targetPosition = new List<Transform>();
    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Value.PLAYER))
        {
            targetPosition.Add(other.transform);
            //Debug.Log(targetPosition.Count);
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Value.PLAYER))
        {
            targetPosition.Remove(other.transform);
        }
    }
    public void ChangeAnimation(string tempAnim)
    {
        if (currAnim != tempAnim)
        {
            _animator.SetTrigger(tempAnim);
            currAnim = tempAnim;
            //Debug.Log(currAnim);
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
        Weapon weaponToThrow = ItemPooling.Instance.SpawnFromPool(WeaponType.axe, WeaponHolder.position, Quaternion.identity).GetComponent<Weapon>();
        weaponToThrow.SetupWeapon(target);
    }
}
