using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState State { get; private set; }

    public void Initialize(PlayerState startingState)
    {
        State = startingState;
        State.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        State.Exit();
        State = newState;
        State.Enter();
    }
}
