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

        float scaleX = Mathf.Sign(-direction.x);
        transform.localScale = new Vector3(scaleX, 1f, 1f);
    }


    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = movementDirection * projectileData.movementVelocity;
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void SetVelocityX(float velocity)
    {
        rb.velocity = movementDirection * projectileData.movementVelocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player") && shooterTag == "Enemy") ||
            (other.CompareTag("Enemy") && shooterTag == "Player"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(projectileData.basicDamage);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            Impact();
        }
    }

    private void Impact()
    {
        Destroy(gameObject);
    }
}

