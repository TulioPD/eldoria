using UnityEngine;

public class Player : Creature
{
    private readonly string faction = "Player";

    public string Faction => faction;

    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerGroundState GroundState { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    [SerializeField]
    private PlayerData playerData;
    #endregion
    #region Components
    public Animator Animator { get; private set; }
    public InputHandler PlayerMovement { get; private set; }
    #endregion
    #region Other Variables
    private Vector2 workspace;
    public Rigidbody2D RB { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    #endregion

    #region Unity Callback Functions
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new PlayerStateMachine();
        IdleState= new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
    }
    
    protected override void Start()
    {
        base.Start();
        FacingDirection = 1;
        RB= GetComponent<Rigidbody2D>();
        PlayerMovement = GetComponent<InputHandler>();
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
    #endregion
    
    #region Check Functions
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    #endregion
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
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
