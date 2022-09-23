using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateIdle : AIState
{
    private float idleTime;
    public AIStateId GetId()
    {
        return AIStateId.idle;
    }
    public void Enter(AIAgent agent)
    {
        //Debug.Log("idle");
        agent.Anim.SetTrigger(Value.CURRENT_ANIM_IDLE);
    }
    public void Update(AIAgent agent)
    {
        if(idleTime < Value.IDLETIME)
        {
            if (agent.enemyRef.ScanningEnemy())
            {
                agent.StateMachine.ChangeState(AIStateId.attack);
            }
            else
            {
                idleTime += Time.deltaTime;
            }
        }
        else
        {
            agent.StateMachine.ChangeState(AIStateId.roaming);
            agent.Anim.SetTrigger(Value.CURRENT_ANIM_RUN);
        }
    }
    public void Exit(AIAgent agent)
    {
        idleTime = 0;
    }
}
