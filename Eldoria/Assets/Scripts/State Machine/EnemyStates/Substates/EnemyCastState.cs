using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCastState : EnemyAbilityState
{
    
    public EnemyCastState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        stateMachine.ChangeState(enemy.IdleState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        enemy.Attack();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocityX(0);
        Debug.Log("CAST STATE");

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
