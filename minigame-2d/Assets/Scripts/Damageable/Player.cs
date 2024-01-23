using UnityEngine;

public class Player : Creature
{
    private readonly string faction = "Player";

    public string Faction => faction;

    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    //public PlayerGroundState GroundState { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    //public PlayerWallGrabState WallGrabState { get; private set; }
    //public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    [SerializeField]
    private PlayerData playerData;
    #endregion
    #region Components
    public Animator Animator { get; private set; }
    public InputHandler InputHandler { get; private set; }
    #endregion
    #region Other Variables
    private Vector2 workspace;
    public Rigidbody2D RB { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
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
        //WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        //WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
    }
    
    protected override void Start()
    {
        base.Start();
        FacingDirection = 1;
        RB= GetComponent<Rigidbody2D>();
        InputHandler = GetComponent<InputHandler>();
        Animator = GetComponent<Animator>();
        StateMachine.Initialize(IdleState);
        // Additional start logic if needed
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
    #region Other Functions
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    private void AnimationTrigger()=> StateMachine.State.AnimationTrigger();
    private void AnimationFinishTrigger()=> StateMachine.State.AnimationFinishTrigger();
    #endregion
    public enum PlayerInputs
    {
        Jump,
        Move,
        Attack,
        Dash,
        Interact,
        Pause
    }
}
