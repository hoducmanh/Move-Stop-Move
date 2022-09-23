using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : Character
{
    public List<Weapon> weapons;
    public AIAgent agent;
    public NavMeshAgent navAgent;
    public Transform trans;
    public Collider Col;
    public bool isAttackable = false;
    Quaternion curRotation;
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
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag(Value.WEAPON))
        {
            //Debug.Log("got hit");
            ChangeAnimation(Value.CURRENT_ANIM_DEAD);
            navAgent.enabled = false;
            agent.enabled = false;
            Col.enabled = false;
            isDead = true;
        }
    }
    public void Attack()
    {
        Vector3 currTarget = Vector3.zero;
        currTarget = targetPosition[0].PlayerTrans.position - meshPlayer.localPosition;
        Throwing(currTarget);
        curRotation = Quaternion.LookRotation(currTarget);
        trans.rotation = curRotation;
    }
    public void GetRandomWeapon()
    {

    }
}
