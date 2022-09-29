using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : Character
{
    public List<Weapon> weapons;
    public AIAgent agent;
    public Transform trans;
    public Collider Col;
    public Collider sphereCol;
    public bool isAttackable = false;
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
        currTarget = (targetPosition[0].PlayerTrans.position - meshPlayer.localPosition).normalized;
        trans.LookAt(currTarget);
        Throwing(currTarget);
    }
    public void GetRandomWeapon()
    {

    }
    
}
