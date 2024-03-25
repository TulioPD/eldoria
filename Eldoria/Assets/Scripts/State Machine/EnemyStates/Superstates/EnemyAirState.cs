using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAirState : EnemyState
{
    public EnemyAirState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }
}
