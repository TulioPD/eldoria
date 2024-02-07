using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Enemy : Creature
{
    #region State Variables
    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyMoveState MoveState { get; private set; }
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
    #endregion
    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; internal set; }
    private Vector2 workspace;
    #endregion
    #region Unity Callback Functions
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine, enemyData, "idle");
        MoveState = new EnemyMoveState(this, StateMachine, enemyData, "move");
    }
    protected override void Start()
    {
        base.Start();
        FacingDirection = 1;
        RB = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        StateMachine.Initialize(IdleState);
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
        Debug.Log("Flipping Enemy!");
    }

    #endregion
    #region Check Functions
    public bool CheckPlayerInMinAgroRange()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(playerCheck.position, enemyData.minAgroDistance, enemyData.whatIsPlayer);

        return playerCollider != null;
    }

    public bool CheckForWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, enemyData.wallCheckDistance, enemyData.whatIsGround);
    }
    #endregion
    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerCheck.position, enemyData.minAgroDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheck.position,wallCheck.position + Vector3.right * -FacingDirection * enemyData.wallCheckDistance);
    }
    #endregion
}
