using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int InitialState = Animator.StringToHash("Idle");

    public override void EnterState(StateManager enemy)
    {
        Debug.Log("This is Enter state");
    }
    public override void UpdateState(StateManager enemy)
    {
        Debug.Log("This is Update State");

        enemy.animator.Play(InitialState);
        enemy.SwitchState(enemy.chaseState);
    }

    public override void OnCollisionEnter(StateManager enemypublic)
    {
        Debug.Log("This is On Collision State");
    }
}
