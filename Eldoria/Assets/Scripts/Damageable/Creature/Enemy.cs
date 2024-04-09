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
    protected Transform playerCheck;
    [SerializeField]
    protected Transform wallCheck;
    [SerializeField]
    protected Transform attackPoint;
    #endregion
    #region Other Variables
    private Coroutine timerCoroutine;
    public Vector2 CurrentVelocity { get; private set; }
    [SerializeField]
    //this is for some reason backwards. fix and then delete *-1s
    public int FacingDirection { get; internal set; }
    public bool isCooldown { get; internal set; }
    private Vector2 workspace;
    public bool aggro=false;
    public bool playerDetected = false;
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
        aggro=CheckAggro();
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

    public virtual void Attack()
    {
        if (!isCooldown)
    {
        Debug.Log("Enemy attacking");
        StartAttackCooldown(enemyData.attackCooldown);
    }
    else
    {
        Debug.Log("Attack on cooldown");
    }
    }
    public void StartAttackCooldown(float cooldownDuration)
    {
        if (!isCooldown)
        {
            isCooldown = true;
            Animator.SetBool("cooldown", true);

            TimerManager.Instance.StartTimer(cooldownDuration, () =>
            {
                isCooldown = false;
                Animator.SetBool("cooldown", false);
            });
        }
    }
    #endregion
    #region Check Functions
    private bool CheckAggro()
    {
        if (CheckPlayerInSight())
        {
            Debug.Log("true");
            return true;
        }
        Debug.Log("false");
        return false;
    }
    public bool CheckPlayerInSight()
{
    if (enemyData == null)
    {
        Debug.LogWarning("EnemyData is not assigned.");
        return false;
    }

    if (CheckPlayerInMinAggroRange())
    {
            Debug.Log("agro");
        Vector2 enemyToPlayer = (FindPlayerPosition() - (Vector2)(transform.position+Vector3.up)).normalized;

        // Check if player is within sight angle
        float angleToPlayer = Vector2.Angle(transform.right * transform.localScale.x, enemyToPlayer);
        if (angleToPlayer <= enemyData.sightAngle / 2f)
        {
                Debug.Log(" ang");
                // Check if player is within sight range
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position+Vector3.up, enemyToPlayer, enemyData.minAgroDistance, enemyData.whatIsPlayer);

            // Check each hit to ensure there are no obstacles between the enemy and the player
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                        Debug.Log("1");
                        return true;
                }
                else if (hit.collider != null && !hit.collider.isTrigger)
                {
                        // An obstacle is blocking the line of sight
                        Debug.Log("2");
                    return false;
                        
                }
            }

            // If no player is detected directly but no obstacles are blocking the line of sight
            return false;
        }
            Debug.Log("no ang");
    }
        Debug.Log("no agro");
        return false;
}


    public Vector2 FindPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            return player.transform.position+Vector3.up;
        }
        else
        {
            return transform.position + transform.right * transform.localScale.x * enemyData.minAgroDistance;
        }
    }

    public bool CheckPlayerInMinAggroRange()
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
    private void OnDrawGizmos()
    {
        if (enemyData == null)
            return;

        Gizmos.color = Color.yellow;

        // Check if player is within aggro range
        Vector2 enemyToPlayer = new Vector2(-FacingDirection, 0);
        if (CheckPlayerInMinAggroRange())
        {
            Vector2 playerPosition = FindPlayerPosition();
            enemyToPlayer = (playerPosition - (Vector2)(transform.position + Vector3.up )).normalized;

            // Draw sight area
            float halfSightAngle = enemyData.sightAngle / 2f;
            Vector2 leftRayDirection = Quaternion.Euler(0, 0, -halfSightAngle) * enemyToPlayer;
            Vector2 rightRayDirection = Quaternion.Euler(0, 0, halfSightAngle) * enemyToPlayer;
            Gizmos.DrawRay(transform.position + Vector3.up , leftRayDirection * enemyData.minAgroDistance);
            Gizmos.DrawRay(transform.position + Vector3.up, rightRayDirection * enemyData.minAgroDistance);
        }

        // Draw circular aggro range
        Gizmos.DrawWireSphere(transform.position, enemyData.minAgroDistance);

        // Draw other gizmos
        DrawOtherGizmos();
    }

    private void DrawOtherGizmos()
    {
        // min aggro range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerCheck.position, enemyData.minAgroDistance);

        // wall check
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * -FacingDirection * enemyData.wallCheckDistance);

        // Player in max Aggro range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(playerCheck.position, enemyData.attackRadius);

        // Player in close range action
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerCheck.position, enemyData.closeRangeActionRadius);
    }
    #endregion
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        StateMachine.ChangeState(TakeDamageState);
    }
    private void AnimationTrigger() => StateMachine.State.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.State.AnimationFinishTrigger();
}
