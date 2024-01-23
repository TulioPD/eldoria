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

    //TODO: Buscar mejor manera de controlar el flip del jugador al entrar y salir. Probablemente simplemente editar sprites desde GIMP
    public override void Enter()
    {
        base.Enter();
        //player.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    public override void Exit ()
    {
        base.Exit();
        //player.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
}
