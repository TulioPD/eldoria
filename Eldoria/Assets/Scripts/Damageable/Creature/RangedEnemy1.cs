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
        if (enemyData == null || attackPoint == null)
        {
            Debug.LogError("data or attackPoint is null.");
            return;
        }

        GameObject firebolt = Instantiate(projectilePrefab, attackPoint.transform.position, attackPoint.rotation);
        if (firebolt == null)
        {
            Debug.LogError("Failed to instantiate projectilePrefab.");
            return;
        }

        Projectile projectile = firebolt.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.Initialize(new Vector2(FacingDirection * -1, 0), this.tag);
        }
    }

}
