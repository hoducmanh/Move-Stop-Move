using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateAttack : AIState
{
    public AIStateId GetId()
    {
        return AIStateId.attack;
    }
    public void Enter(AIAgent agent)
    {
        ThrowWeapon(agent);
    }
    public void Update(AIAgent agent)
    {
            
    }
    public void Exit(AIAgent agent)
    {
        agent.enemyRef.WeaponHolder.SetActive(true);
    }
    private void ThrowWeapon(AIAgent agent)
    {
        agent.Anim.SetTrigger(Value.CURRENT_ANIM_ATTACK);
        agent.enemyRef.WeaponHolder.SetActive(false);
        agent.enemyRef.EnemyAttack();
        agent.StateMachine.ChangeState(AIStateId.roaming);
    }
}
