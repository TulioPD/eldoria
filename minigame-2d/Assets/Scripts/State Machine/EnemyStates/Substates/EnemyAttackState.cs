using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAttackState : EnemyAbilityState
{
    private bool isPlayerInAttackRange;
    private bool inCooldown;
    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
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
        isPlayerInAttackRange = enemy.CheckPlayerInMaxAttackRange();
        
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocityX(0f);
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
        if (!enemy.isCooldown)
        {
            // Attack the player

            // Start the attack cooldown
            enemy.StartAttackCooldown(enemyData.attackCooldown);
        }
        if (!isPlayerInAttackRange)
        {
            stateMachine.ChangeState(enemy.IdleState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
}
