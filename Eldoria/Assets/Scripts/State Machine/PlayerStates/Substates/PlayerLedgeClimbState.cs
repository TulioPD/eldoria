using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 detectedPosition;
    private Vector2 cornerPosition;
    private Vector2 startPosition;
    private Vector2 stopPosition;

    private bool isHanging;
    private bool isClimbing;

    private int xInput;
    private int yInput;
    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.Animator.SetBool("climbLedge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        isHanging = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()    
    {
        base.Enter();
        player.SetVelocityZero();
        player.transform.position = detectedPosition;
        cornerPosition = player.DetermineCornerPosition();
        startPosition.Set(cornerPosition.x - (player.FacingDirection * playerData.startOffset.x), cornerPosition.y - playerData.startOffset.y);
        stopPosition.Set(cornerPosition.x + (player.FacingDirection * playerData.stopOffset.x), cornerPosition.y + playerData.stopOffset.y);

        player.transform.position = startPosition;
    }

    public override void Exit()
    {
        base.Exit();
        isHanging = false;
        if (isClimbing)
        {
            player.transform.position = stopPosition;
            isClimbing = false;
        }
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
            
        if (isAnimationFinished){
            stateMachine.ChangeState(player.IdleState);
        }
        else
        {
            xInput = player.InputHandler.NormalizedInputX;
            yInput = player.InputHandler.NormalizedInputY;
            player.SetVelocityZero();
            player.transform.position = startPosition;

            if (xInput == player.FacingDirection && isHanging && !isClimbing)
            {
                isClimbing = true;
                player.Animator.SetBool("climbLedge", true);
            }

            else if (yInput == -1 && isHanging && !isClimbing)
            {
                stateMachine.ChangeState(player.AirState);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetDetectedPosition(Vector2 pos) => detectedPosition = pos;
}
