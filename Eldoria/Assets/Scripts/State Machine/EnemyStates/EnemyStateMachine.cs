using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState State { get; private set; }
    public void Initialize(EnemyState startingState)
    {
        State = startingState;
        State.Enter();
    }

    public void ChangeState(EnemyState newState)
    {
        State.Exit();
        State = newState;
        State.Enter();
    }
}
