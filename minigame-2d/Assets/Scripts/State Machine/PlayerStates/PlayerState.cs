using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    private string animBoolName;
    protected float startTime;
    protected bool isAnimationFinished;


    protected PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        player.Animator.SetBool(animBoolName, true);
        startTime = Time.time;
        //Debug.Log(animBoolName);
        isAnimationFinished = false;
    }
    public virtual void Exit()
    {
        player.Animator.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void AnimationTrigger() => isAnimationFinished = false;
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
    public virtual void DoChecks() { }

}
