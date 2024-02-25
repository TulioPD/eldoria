using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamageState : EnemyState
{
    public EnemyTakeDamageState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        if (enemy.ShouldDie())
        {
            enemy.StateMachine.ChangeState(enemy.DeadState);
        }
        else
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
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
