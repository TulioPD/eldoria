using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    private bool isGrounded;
    private int xInput;
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded=player.CheckIfGrounded();
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
        xInput=player.InputHandler.NormalizedInputX;
        if (isGrounded&&player.CurrentVelocity.y<.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity * xInput);
            player.Animator.SetFloat("yVelocity",player.CurrentVelocity.y);
            //player.Animator.SetFloat("xVelocity",Mathf.Abs(player.CurrentVelocity.x));
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}


