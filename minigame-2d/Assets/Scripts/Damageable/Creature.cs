using UnityEngine;

public abstract class Creature : MonoBehaviour, IDamageable
{
    // Serialized fields for Unity Editor
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int resistance;
    [SerializeField] protected int damageTaken;

    public string Name { get; private set; }

    protected virtual void Start()
    {
        // Initialization logic here
        Debug.Log($"{Name} has been created with {health} health, {maxHealth} max health, {resistance} resistance, and {damageTaken} damage.");
    }

    protected virtual void Update()
    {
        // Update logic here
    }

    protected virtual void FixedUpdate()
    {
        // Physics update logic here
    }

    protected virtual void Awake()
    {
        // Initialization logic here
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
        // Additional death logic if needed
    }
}
