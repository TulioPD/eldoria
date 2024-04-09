using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbilityState : EnemyState
{
    protected bool isAbilityDone;
    private bool isGrounded;
    public EnemyAbilityState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded=enemy.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone=false;
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
        if(isAbilityDone)
        {
            if(isGrounded&&enemy.CurrentVelocity.y<.01f&&enemy.isCooldown)
            {
                stateMachine.ChangeState(enemy.IdleState);
            } else
            {
                stateMachine.ChangeState(enemy.AirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
