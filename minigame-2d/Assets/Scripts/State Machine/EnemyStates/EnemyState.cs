using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;
    protected EnemyData enemyData;
    private string animBoolName;
    protected float startTime;
    protected bool isAnimationFinished;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.enemyData = enemyData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        enemy.Animator.SetBool(animBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
    }

    public virtual void Exit()
    {
        enemy.Animator.SetBool(animBoolName, false);
    }
    public virtual void FrameUpdate()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void AnimationTrigger() => isAnimationFinished = false;
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
    public virtual void DoChecks() { }


}
