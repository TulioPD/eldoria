using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidEnemy : Enemy
{
    #region State Variables
    public EnemyMoveState MoveState { get; private set; }
    public EnemyJumpState JumpState { get; private set; }
    public EnemyAirState AirState { get; private set; }
    public EnemyLandState LandState { get; private set; }
    #endregion
    #region Components
    #endregion
    #region Check Variables
    //[SerializeField]
    //protected Transform wallCheck;
    [SerializeField]
    protected Transform groundCheck;
    #endregion
    #region Other Variables
    #endregion
    #region Unity Callback Functions
    protected override void Awake()
    {
        base.Awake();
        MoveState = new EnemyMoveState(this, StateMachine, enemyData, "move");
        JumpState = new EnemyJumpState(this, StateMachine, enemyData, "air");
        AirState = new EnemyAirState(this, StateMachine, enemyData, "air");
        LandState = new EnemyLandState(this, StateMachine, enemyData, "land");
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    #endregion
}
