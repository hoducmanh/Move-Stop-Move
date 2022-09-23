using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateRoaming : AIState
{
    
    public AIStateId GetId()
    {
        return AIStateId.roaming;

    }
    public void Enter(AIAgent agent)
    {
        //Debug.Log("run");
        agent.Anim.SetTrigger(Value.CURRENT_ANIM_RUN);
        agent.NavAgent.destination = GetRandomDir();
    }
    public void Update(AIAgent agent)
    {
        //Debug.Log(agent.NavAgent.destination);
        float dis = (agent.enemyRef.trans.position - agent.NavAgent.destination).magnitude;
        if (dis < 0.01)
        {
            //Debug.Log(agent.enemyRef.navAgent.velocity.sqrMagnitude);
            agent.StateMachine.ChangeState(AIStateId.idle);
        }
    }
    public void Exit(AIAgent agent)
    {
    }
    private Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-20f,20f), 0, Random.Range(-20f,20f)); 
    }
}
