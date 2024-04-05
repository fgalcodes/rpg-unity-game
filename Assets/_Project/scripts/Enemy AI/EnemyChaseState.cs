using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private readonly int chase = Animator.StringToHash("FastRun");
    public override void EnterState(StateManager enemy)
    {
        Debug.Log("This is Enter state");
        enemy.controller.InitChase = true;
    }
    public override void UpdateState(StateManager enemy)
    {

        enemy.animator.Play(chase);
        enemy.controller.ChasePlayer();
        //enemy.SwitchState(enemy.chaseState);
    }

    public override void OnCollisionEnter(StateManager enemypublic)
    {
        Debug.Log("This is On Collision State");
    }

}
