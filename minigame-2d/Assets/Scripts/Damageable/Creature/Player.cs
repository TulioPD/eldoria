using UnityEngine;

public class Player : Creature
{
    #region State Variables
    
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerTakeDamageState TakeDamageState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    [SerializeField]
    private PlayerData playerData;
    #endregion
    #region Components
    public Animator Animator { get; private set; }
    public InputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Inventory Inventory { get; private set; }
    #endregion
    #region Other Variables
    private Vector2 workspace;
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; internal set; }
    #endregion
    #region Check Variables
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    #endregion

    #region Unity Callback Functions
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new PlayerStateMachine();
        IdleState= new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "air");
        AirState = new PlayerAirState(this, StateMachine, playerData, "air");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        TakeDamageState = new PlayerTakeDamageState(this, StateMachine, playerData, "takeDamage");
        DeadState = new PlayerDeadState(this, StateMachine, playerData, "dead");
    }
    
    protected override void Start()
    {
        base.Start();
        FacingDirection = 1;
        RB= GetComponent<Rigidbody2D>();
        InputHandler = GetComponent<InputHandler>();
        Inventory = GetComponent<Inventory>();
        Animator = GetComponent<Animator>();
        StateMachine.Initialize(IdleState);
        this.maxHealth=playerData.maxHealth;
        this.health = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
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
    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    #endregion
    
    #region Check Functions
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);

    }
    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);

    }
    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);

    }
    #endregion
    #region Debug Functions
    private void OnDrawGizmos()
    {
        // Visualize ground check radius
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, playerData.groundCheckRadius);

        // Visualize wall check
            Gizmos.color = Color.red;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * FacingDirection * playerData.wallCheckDistance);

        // Visualize ledge check
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + Vector3.right * FacingDirection * playerData.wallCheckDistance);
    }

    private void OnDrawGizmosSelected()
    {
    }
    #endregion
    #region Other Functions
    internal void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        StateMachine.ChangeState(TakeDamageState);
        if (healthBar != null)
        {
            healthBar.SetHealth(health);
        }
    }
    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
        float xDist = xHit.distance;
        workspace.Set(xDist * FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workspace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y, playerData.whatIsGround);
        float yDist = yHit.distance;
        workspace.Set(wallCheck.position.x + (xDist * FacingDirection), ledgeCheck.position.y - yDist);
        return workspace;
    }

    public void AddGemsToInventory(int amount)=>Inventory.AddGems(amount);
    private void AnimationTrigger()=> StateMachine.State.AnimationTrigger();
    private void AnimationFinishTrigger()=> StateMachine.State.AnimationFinishTrigger();
    #endregion
}
