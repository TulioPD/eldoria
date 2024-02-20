using UnityEngine;

public abstract class Creature : MonoBehaviour, IDamageable
{
    public int health;
    public int maxHealth;
    [SerializeField] protected int resistance;
    [SerializeField] protected int damageTaken;
    public PlayerStateMachine StateMachine { get; private set; }
    

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
        StateMachine = new PlayerStateMachine();
    }

    public virtual void TakeDamage(int damageAmount)=> health -= damageAmount;
    

    public bool ShouldDie()=> health <= 0;
    

    public void Die(float lifetime)=>Destroy(gameObject, lifetime);
}
