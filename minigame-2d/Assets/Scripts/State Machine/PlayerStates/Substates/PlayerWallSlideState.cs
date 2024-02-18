using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.SetVelocityY(-playerData.wallSlideVelocity);
    }

    public override void Enter()
    {
        base.Enter();
        player.transform.localScale=new Vector2(player.transform.localScale.x*-1, player.transform.localScale.y);
    }
    public override void Exit ()
    {
        base.Exit();
        player.transform.localScale = new Vector2(player.transform.localScale.x * -1, player.transform.localScale.y);
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
    
    
}
