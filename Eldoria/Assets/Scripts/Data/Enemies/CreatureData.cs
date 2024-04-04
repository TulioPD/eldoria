using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCreatureData", menuName = "Data/Creature Data/Base Data")]
public class CreatureData : ScriptableObject
{
    [Header("identifiers")]
    public int id;
    [Header("Stats")]
    public int maxHealth;
    public float movementVelocity;
    public float attackRange;
    public int basicDamage;
    public float lifeTime;
    public float nextAttackTime;
    public float mitigation;
    public float attackRadius;
    public float attackDamage;
    public float attackRate;
    public float attackCooldown;
}
