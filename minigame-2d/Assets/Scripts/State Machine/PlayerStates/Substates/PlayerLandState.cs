using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        //Comentado para testear Enter vs LogicUpdate. Debería de hacer lo mismo en ambos estados. Volver a descomentar si en el futuro se producen errores con interacciones de animaciones al comprobarse más de una vez por frame
        base.Enter();
        //if (player.InputHandler.NormalizedInputX!=0)
        //{
        //    //player.CheckIfShouldFlip(player.InputHandler.NormalizedInputX);
        //    stateMachine.ChangeState(player.MoveState);
        //}
        //else
        //{
        //    stateMachine.ChangeState(player.IdleState);
        //}
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
        if (player.InputHandler.NormalizedInputX != 0)
        {
            //player.CheckIfShouldFlip(player.InputHandler.NormalizedInputX);
            stateMachine.ChangeState(player.MoveState);
        }
        else //TODO: comprobar si la animación ha terminado y generar polvo. Cambiar a else if. Será necesario llamar a la función desde el final de la animación desde el editor de Unity. No necesario en el estado actual para que funcione
        {
            //Debug.Log("Animation Finished - Land player");
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

    