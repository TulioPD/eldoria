//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerWallClimbState : PlayerTouchingWallState
//{
    

//    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
//    {
//    }

//    public override void AnimationFinishTrigger()
//    {
//        base.AnimationFinishTrigger();
//    }

//    public override void AnimationTrigger()
//    {
//        base.AnimationTrigger();
//    }

//    public override void DoChecks()
//    {
//        base.DoChecks();
        
//    }

//    public override void Enter()
//    {
//        base.Enter();
//    }

//    public override void Exit()
//    {
//        base.Exit();
//    }

//    public override void FrameUpdate()
//    {
//        base.FrameUpdate();
//    }

//    public override void LogicUpdate()
//    {
//        base.LogicUpdate();
//        player.SetVelocityY(playerData.wallClimbVelocity);

//        if (yInput != 1)
//        {
//            stateMachine.ChangeState(player.WallGrabState);
//        }
//    }

//    public override void PhysicsUpdate()
//    {
//        base.PhysicsUpdate();
//    }
//}
