using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : CreatureData
{
    [Header("Jump State")]
    public float jumpVelocity;
    public int amountOfJumps;
    [Header("Attack State")]
    public LayerMask whatIsPlayer;
    [Header("Die State")]
    public float dieTime;
    [Header("Checks")]
    public float minAgroDistance;
    public float wallCheckDistance;
    public float ledgeCheckDistance;
    public float groundCheckDistance;
    public float closeRangeActionRadius;
    public LayerMask whatIsGround;

    [Header("Other")]
    public float sightAngle = 45f;

}
