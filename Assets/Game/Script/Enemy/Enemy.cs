using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : Character
{
    public AIAgent agent;
    public NavMeshAgent navAgent;
    public Transform trans;
    public Collider Col;
    public bool isAttackable = false;
    public bool ScanningEnemy()
    {
        if(targetPosition.Count > 0)
        {
            return true;
        }
        else return false;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        //Debug.Log("got hit");
        if (other.CompareTag(Value.WEAPON))
        {
            Debug.Log("got hit");
            ChangeAnimation(Value.CURRENT_ANIM_DEAD);
            navAgent.enabled = false;
            agent.enabled = false;
            Col.enabled = false;

        }
    }
}
