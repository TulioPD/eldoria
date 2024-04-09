using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundState : EnemyState
{
    private bool canDoBasicAttack;
    private bool playerInAttackRange;
    public EnemyGroundState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
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
        if (!enemy.CheckPlayerInCloseRangeAction()&&enemy.CheckPlayerInMaxAttackRange())
        {
            playerInAttackRange = true;
            Debug.Log("Player in attack range");
        }
        else
        {
            playerInAttackRange = false;
        }
        
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
        if (enemy.CheckPlayerInMinAggroRange())
        {
            if ((enemy.FindPlayerPosition().x - enemy.transform.position.x) > 0 && enemy.FacingDirection == 1)
            {
                enemy.Flip();
                enemy.FacingDirection = -1;
            }
            else if ((enemy.FindPlayerPosition().x - enemy.transform.position.x) < 0 && enemy.FacingDirection == -1)
            {
                enemy.Flip();
                enemy.FacingDirection = 1;
            }
            if (!enemy.CheckPlayerInMaxAttackRange()&&!enemy.CheckForWall())
            {
                stateMachine.ChangeState(enemy.MoveState);
            }
        }
        if (playerInAttackRange && !enemy.isCooldown )
        {
            stateMachine.ChangeState(enemy.AttackState);
        }
        else if (enemy.isCooldown&&playerInAttackRange&&enemy.CheckPlayerInMinAggroRange())
        {
            stateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
