using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

public class Enemy : Creature
{
    #region State Variables
    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyMoveState MoveState { get; private set; }
    public EnemyJumpState JumpState { get; private set; }
    public EnemyAirState AirState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyDeadState DeadState { get; private set; }
    public EnemyTakeDamageState TakeDamageState { get; private set; }
    #endregion
    #region Components
    [SerializeField]
    protected EnemyData enemyData;
    public Animator Animator { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion
    #region Check Variables
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform attackPoint;
    #endregion
    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; internal set; }
    public bool isCooldown { get; internal set; }
    private Vector2 workspace;
    #endregion
    #region Unity Callback Functions
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine, enemyData, "idle");
        MoveState = new EnemyMoveState(this, StateMachine, enemyData, "move");
        JumpState = new EnemyJumpState(this, StateMachine, enemyData, "jump");
        AirState = new EnemyAirState(this, StateMachine, enemyData, "air");
        AttackState = new EnemyAttackState(this, StateMachine, enemyData, "attack");
        DeadState = new EnemyDeadState(this, StateMachine, enemyData, "dead");
        TakeDamageState = new EnemyTakeDamageState(this, StateMachine, enemyData, "takeDamage");
    }
    protected override void Start()
    {
        base.Start();
        FacingDirection = 1;
        RB = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        StateMachine.Initialize(IdleState);
        this.maxHealth = enemyData.maxHealth;
        this.health = maxHealth;
    }
    protected override void Update()
    {
        base.Update();
        CurrentVelocity = RB.velocity;
        StateMachine.State.LogicUpdate();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        StateMachine.State.PhysicsUpdate();
    }
    #endregion
    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion
    #region Other Functions
    public void Flip()
    {
        transform.Rotate(0.0f, 180.0f * FacingDirection, 0.0f);
    }
    public void Attack()
    {
        GameObject firebolt = Instantiate(enemyData.projectilePrefab, attackPoint.transform.position, attackPoint.rotation);
        Projectile projectile = firebolt.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.Initialize(new Vector2(FacingDirection*-1, 0),this.tag);
        }
    }
    public void StartAttackCooldown(float cooldownDuration)
    {
        if (!isCooldown)
        {
            StartCoroutine(AttackCooldown(enemyData.attackCooldown));
        }
    }
    private IEnumerator AttackCooldown(float cooldownDuration)
    {
        isCooldown = true;
        Animator.SetBool("cooldown", true);
        Animator.SetBool("idle", true);
        yield return new WaitForSeconds(cooldownDuration);
        isCooldown = false;
        Animator.SetBool("cooldown", false);
        Animator.SetBool("idle", false);
    }

    #endregion
    #region Check Functions
    public bool CheckPlayerInMinAgroRange()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(playerCheck.position, enemyData.minAgroDistance, enemyData.whatIsPlayer);

        return playerCollider != null;
    }
    public bool CheckPlayerInMaxAttackRange()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(playerCheck.position, enemyData.attackRadius, enemyData.whatIsPlayer);
        return playerCollider != null;
    }
    public bool CheckPlayerInCloseRangeAction()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(playerCheck.position, enemyData.closeRangeActionRadius, enemyData.whatIsPlayer);
        return playerCollider != null;
    }

    public bool CheckForWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, enemyData.wallCheckDistance, enemyData.whatIsGround);
    }
    public bool CheckIfGrounded()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.down, enemyData.groundCheckDistance, enemyData.whatIsGround);
    }
    #endregion
    #region Gizmos
    //private void OnDrawGizmos()
    //{
    //    //min agro range
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(playerCheck.position, enemyData.minAgroDistance);

    //    //wallcheck
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(wallCheck.position,wallCheck.position + Vector3.right * -FacingDirection * enemyData.wallCheckDistance);

    //    //Player in max Agro range
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(playerCheck.position, enemyData.attackRadius);

    //    //Player in close range action
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(playerCheck.position, enemyData.closeRangeActionRadius);
    //}
    #endregion
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        StateMachine.ChangeState(TakeDamageState);
    }
    private void AnimationTrigger() => StateMachine.State.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.State.AnimationFinishTrigger();
}
