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
        agent.Anim.SetTrigger(Value.CURRENT_ANIM_RUN);
    }
    public void Update(AIAgent agent)
    {

    }
    public void Exit(AIAgent agent)
    {

    }
    private Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f,1f), 1, Random.Range(-1f,1f)); 
    }

}
