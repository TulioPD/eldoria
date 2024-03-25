using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newProjectileData", menuName = "Data/Projectile Data/Base Data")]
public class ProjectileData : ScriptableObject
{
    [Header("Movement")]
    public float movementVelocity;
    public float lifeTime;
    [Header("Damage and status")]
    public int basicDamage;
    public int basicKnockback;
    public int basicStun;
    public int basicSlow;
}
