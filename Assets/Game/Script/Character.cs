using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Character player;
    public GameObject tempPlayer;
    public Transform meshPlayer;
    public Animator _animator;
    public Transform PlayerTrans;
    public GameObject ThrowPos;
    public GameObject WeaponHolder;
    public WeaponType weaponTag;
    public WeaponSkinType weaponSkinTag;
    public bool isDead;
    public float speed;
    protected string currAnim = Value.CURRENT_ANIM_IDLE;
    protected List<Character> targetPosition = new List<Character>();
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Value.PLAYER))
        {
            Debug.Log(other + "enter");
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
            Debug.Log(other + "exit");
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
        Weapon weaponToThrow = ItemPooling.Instance.SpawnWeaponFromPool(WeaponType.axe, WeaponSkinType.Axe_0, ThrowPos.transform.position, Quaternion.identity).GetComponent<Weapon>();
        weaponToThrow.SetRotation(); 
        weaponToThrow.SetupWeapon(target);
    }
    public void CollideWithWeapon(Collider other)
    {
        if (other.CompareTag(Value.WEAPON))
        {
            Debug.Log("hit");
            ChangeAnimation(Value.CURRENT_ANIM_DEAD);
            Dead();
            player.enabled = false;
            navAgent.enabled = false;
            isDead = true;
        }
    }
}
