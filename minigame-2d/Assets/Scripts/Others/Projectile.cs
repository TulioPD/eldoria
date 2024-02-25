using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected ProjectileData projectileData;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private string shooterTag;
    private float lifetime;

    public void Initialize(Vector2 direction, string shooterTag)
    {
        movementDirection = direction.normalized;
        this.shooterTag = shooterTag;
        lifetime = projectileData.lifeTime;
    }

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SetVelocityX(projectileData.movementVelocity);
    }

    private void Update()
    {
        rb.MovePosition(rb.position + movementDirection * projectileData.movementVelocity * Time.deltaTime);

        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void SetVelocityX(float velocity)
    {
        rb.velocity = new Vector2(velocity, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") && shooterTag == "Enemy") ||
            (collision.gameObject.CompareTag("Enemy") && shooterTag == "Player"))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(projectileData.basicDamage);
            }
            Destroy(gameObject);
        }
    }
}
