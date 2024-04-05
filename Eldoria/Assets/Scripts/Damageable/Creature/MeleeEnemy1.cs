using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy1 : Enemy
{
    public override void Attack()
    {
        base.Attack();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, enemyData.attackRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                IDamageable player = collider.gameObject.GetComponent<IDamageable>();
                if (player != null)
                {
                    player.TakeDamage(enemyData.basicDamage);
                }
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
