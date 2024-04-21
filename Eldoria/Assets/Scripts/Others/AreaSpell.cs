using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AreaSpell : MonoBehaviour
{
    [SerializeField] private int damage = 50;

    private void ApplyDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0f);
        foreach (Collider2D collider in colliders.Where(c => c.CompareTag("Player")))
        {
            IDamageable player = collider.GetComponent<IDamageable>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}

