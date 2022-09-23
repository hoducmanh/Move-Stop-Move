using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public AIStateMachine StateMachine;
    public AIStateId InitialState;
    public Animator Anim;
    public NavMeshAgent NavAgent;
    public Enemy enemyRef;
    public AIStateId currState;
    void Start()
    {
        StateMachine = new AIStateMachine(this);
        StateMachine.RegisterState(new AIStateRoaming());
        StateMachine.RegisterState(new AIStateIdle());
        StateMachine.RegisterState(new AIStateAttack());
        StateMachine.ChangeState(InitialState);
    }

    void Update()
    {
        StateMachine.Update();
    }
}
