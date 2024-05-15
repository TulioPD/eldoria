using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy1 : Enemy
{
    public GameObject projectilePrefab;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        base.Attack();
        if (enemyData == null || attackPoint == null)
        {
            return;
        }

        Vector2 directionToPlayer = (FindPlayerPosition() - (Vector2)attackPoint.position).normalized;

        GameObject firebolt = Instantiate(projectilePrefab, attackPoint.transform.position, Quaternion.identity);
        if (firebolt == null)
        {
            return;
        }

        Projectile projectile = firebolt.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.Initialize(directionToPlayer, this.tag);
        }
    }


}
