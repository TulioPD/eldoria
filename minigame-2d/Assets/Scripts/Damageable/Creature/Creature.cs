using UnityEngine;

public abstract class Creature : MonoBehaviour, IDamageable
{
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int resistance;
    [SerializeField] protected int damageTaken;

    public string Name { get; protected set; }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void Awake()
    {
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
    }

    public bool ShouldDie()
    {
        return health <= 0;
    }

    public void Die()
    {
    }
}
