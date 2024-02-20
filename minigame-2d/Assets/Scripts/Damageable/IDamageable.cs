public interface IDamageable
{
    void TakeDamage(int damageAmount);
    bool ShouldDie();
    void Die(float lifetime);

}
