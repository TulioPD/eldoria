using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    [Header("Movement State")]
    public float movementVelocity;

    [Header("Jump State")]
    public float jumpVelocity;
    public int amountOfJumps;

    [Header("In Air State")]
    [Header("Attack State")]
    public float attackRadius;
    public float attackDamage;
    public float attackRate;
    public float attackCooldown;
    public LayerMask whatIsPlayer;

    [Header("Die State")]
    public float dieTime;

}
