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
        isPlayerInMinAgroRange = enemy.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("EnemyMoveState - Chasing player");
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
        if (!isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.IdleState);
        }
        playerPosition = GameObject.Find("Player").transform.position;
        if ((playerPosition.x - enemy.transform.position.x) > 0 && enemy.FacingDirection == 1)
        {
            enemy.Flip();
            enemy.FacingDirection = -1;
            //Debug.Log("Enemy looking left");
        }
        else if ((playerPosition.x - enemy.transform.position.x) < 0 && enemy.FacingDirection == -1)
        {
            enemy.Flip();
            enemy.FacingDirection = 1;
            //Debug.Log("Enemy looking right");
        }
        
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
            enemy.SetVelocityX(0);
        }
    }
    private void ChasePlayer(Vector3 directionToPlayer)
    {
        enemy.SetVelocityX(directionToPlayer.x * enemyData.movementVelocity);

    }
}
