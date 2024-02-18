using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    private bool isGrounded;
    private bool isTouchingWall;
    private int xInput;
    private bool jumpInput;
    private bool grabInput;
    private bool jumpInputStop;
    private bool coyoteTime;
    private bool isJumping;
    private bool isTouchingLedge;
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
        isTouchingWall=player.CheckIfTouchingWall();
        isTouchingLedge=player.CheckIfTouchingLedge();

        if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
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
        CheckCoyoteTime();
        xInput=player.InputHandler.NormalizedInputX;
        jumpInput=player.InputHandler.JumpInput;
        jumpInputStop=player.InputHandler.JumpInputStop;
        CheckJumpMultiplier();
        
        if (isGrounded&&player.CurrentVelocity.y<.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (isTouchingWall && !isTouchingLedge)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (isTouchingWall && xInput == player.FacingDirection && player.CurrentVelocity.y <= 0f)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity * xInput);
            player.Animator.SetFloat("yVelocity",player.CurrentVelocity.y);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void CheckCoyoteTime()
    {
        if(coyoteTime&& Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime=false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }
    public void StartCoyoteTime() => coyoteTime=true;
    public void SetIsJumping() => isJumping=true;
    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }
}


