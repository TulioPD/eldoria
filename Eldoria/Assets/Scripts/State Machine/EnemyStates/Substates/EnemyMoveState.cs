using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyGroundState
{
    private bool isPlayerInMinAgroRange;
    private Vector3 playerPosition;
    
    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
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
        isPlayerInMinAgroRange = enemy.CheckPlayerInMinAggroRange();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocityX(enemyData.movementVelocity);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.SetVelocityX(0f);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        playerPosition = GameObject.Find("Player").transform.position;
        
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Vector3 directionToPlayer= playerPosition- enemy.transform.position;
        if (!enemy.CheckForWall())
        {
            ChasePlayer(directionToPlayer);
        }
        else
        {
            stateMachine.ChangeState(enemy.IdleState);
            enemy.SetVelocityX(0);
        }
    }
    private void ChasePlayer(Vector3 directionToPlayer)
    {
        enemy.SetVelocityX(directionToPlayer.x * enemyData.movementVelocity);

    }
}
