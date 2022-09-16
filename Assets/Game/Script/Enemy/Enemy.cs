using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : Character
{
    public AIAgent agent;
    public NavMeshAgent navAgent;
    public Transform targetTrans;
    public Collider Col;
    //private bool canMove;
    public Animator animator;

    //private void GameManagerOnGameStateChanged(GameManager.GameState state)
    //{
    //    switch (state)
    //    {
    //        case GameManager.GameState.Playing:
    //            agent.StateMachine.ChangeState(AIStateId.collectBrick);
    //            break;
    //        default:
    //            break;
    //    }
    //}
    
}
