using UnityEngine;
using UnityEngine.SceneManagement;

public class HybridEnemy : MeleeEnemy1
{
    [SerializeField]
    private bool isRangedAttackOnCooldown = false;
    [SerializeField]
    private GameObject spell;
    private float rangedAttackCooldown=5f;
    public bool canCast;

    public EnemyCastState CastState { get; private set;}

    protected override void Start()
    {
        base.Start();
        CastState = new EnemyCastState(this, StateMachine, enemyData, "cast");
    }
    public override void Attack()
    {
        base.Attack();
        if (canCast)
        {
            Cast();
        }
    }


    protected override void Update()
    {
        base.Update();

        if (Animator.GetBool("dead"))
        {
            ScreenFader.Instance.FadeToBlack(2f);

            TimerManager.Instance.StartTimer(2f, () =>
            {
                SceneManager.LoadScene("MainMenu");
            });
        }

        if (CheckPlayerInCloseRangeAction() && !CheckPlayerInMaxAttackRange() && !isRangedAttackOnCooldown)
        {
            canCast = true;
            StateMachine.ChangeState(CastState);
            StartRangedAttackCooldown(rangedAttackCooldown);
        }
        else if (CheckPlayerInMaxAttackRange())
        {
            canCast = false;
        }
    }

    public void Cast()
    {
        Vector3 playerPosition = FindPlayerPosition();
        Vector2 spawnPosition = new Vector2(playerPosition.x +1f, playerPosition.y + 4.5f);
        Instantiate(spell, spawnPosition, Quaternion.identity);
    }

    private void StartRangedAttackCooldown(float cooldownDuration)
    {
        isRangedAttackOnCooldown = true;
        TimerManager.Instance.StartTimer(cooldownDuration, () =>
        {
            isRangedAttackOnCooldown = false;
        });
    }
}
