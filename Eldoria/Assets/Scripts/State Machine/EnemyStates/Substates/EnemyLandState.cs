using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLandState : EnemyGroundState
{
    public EnemyLandState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }
}
