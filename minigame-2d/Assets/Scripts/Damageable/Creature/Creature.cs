using UnityEngine;

public abstract class Creature : MonoBehaviour, IDamageable
{
    // Serialized fields for Unity Editor
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int resistance;
    [SerializeField] protected int damageTaken;

    public string Name { get; protected set; }

    protected virtual void Start()
    {
        Debug.Log($"{Name} has been created with {health} health, {maxHealth} max health, {resistance} resistance, and {damageTaken} damage.");
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
        Debug.Log($"{Name} takes {damageAmount} damage. Remaining health: {health}");
    }

    public bool ShouldDie()
    {
        return health <= 0;
    }

    public void Die()
    {
        Debug.Log($"{Name} has died.");
    }
}
