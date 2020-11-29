using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacter : MonoBehaviour
{
    #region State Variables
    public SwordCharacterIdleState IdleState { get; private set; }
    public SwordCharacterMoveState MoveState { get; private set; }
    public SwordCharaterStateMachine StateMachine { get; private set; }
    public SwordCharacterJumpState JumpState { get; private set; }
    public SwordCharacterAirState AirState { get; private set; }
    public SwordCharacterLandState LandState { get; private set; }
    public SwordCharacterWallSlideState WallSlideState { get; private set; }
    public SwordCharacterWallGrabState WallGrabState { get; private set; }
    public SwordCharacterWallClimbState WallClimbState { get; private set; }

    [SerializeField]
    private SwordCharacterData _characterData;
    #endregion

    #region Components
    public SwordCharacterInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }
    #endregion

    #region Check Transforms
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform _wallCheck;
    #endregion

    #region Player Variables
    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 _workSpace;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new SwordCharaterStateMachine();
        InputHandler = GetComponent<SwordCharacterInputHandler>();
        IdleState = new SwordCharacterIdleState(this, StateMachine, _characterData, "idle");
        MoveState = new SwordCharacterMoveState(this, StateMachine, _characterData, "move");
        JumpState = new SwordCharacterJumpState(this, StateMachine, _characterData, "inAir");
        AirState = new SwordCharacterAirState(this, StateMachine, _characterData, "inAir");
        LandState = new SwordCharacterLandState(this, StateMachine, _characterData, "land");
        WallSlideState = new SwordCharacterWallSlideState(this, StateMachine, _characterData, "wallSlide");
        WallGrabState = new SwordCharacterWallGrabState(this, StateMachine, _characterData, "wallGrab");
        WallClimbState = new SwordCharacterWallClimbState(this, StateMachine, _characterData, "wallClimb");
    }

    private void Start()
    {
        FacingDirection = 1;
        RB = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        //Debug.DrawRay(_wallCheck.position, Vector2.right * FacingDirection * _characterData.WallCheckDistance, Color.cyan);
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate() => StateMachine.CurrentState.PhysicsUpdate();
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        _workSpace.Set(velocity, CurrentVelocity.y);
        RB.velocity = _workSpace;
        CurrentVelocity = _workSpace;
    }

    public void SetVelocityY(float velocity)
    {
        _workSpace.Set(CurrentVelocity.x, velocity);
        RB.velocity = _workSpace;
        CurrentVelocity = _workSpace;
    }
    #endregion

    #region Check Functions
    public bool CheckIfTouchingGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, _characterData.GroundCheckRadius, _characterData.WhatIsGround);
    }

    public void CheckIfShouldFlip(int XInput)
    {
        if (XInput != 0 && XInput != FacingDirection) Flip();
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _characterData.WallCheckDistance, _characterData.WhatIsGround);
    }

    public bool CheckIfIsGrabbable()
    {
        return Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _characterData.WallCheckDistance, _characterData.WhatIsGrabbable);
    }
    #endregion

    #region Other Functions
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion

    #region Gizmos
#if UNITY_EDITOR
    void OnGUI()
    {
        string state = StateMachine.CurrentState.ToString();
        GUI.Box(new Rect(0, 0, 200, 25), state);

        string checkGrabbable = "Grabbable : " + CheckIfIsGrabbable();
        GUI.Box(new Rect(205, 0, 125, 25), checkGrabbable);

        string checkGround = "Ground : " + CheckIfTouchingGround();
        GUI.Box(new Rect(335, 0, 125, 25), checkGround);

        string checkWall = "Wall : " + CheckIfTouchingWall();
        GUI.Box(new Rect(465, 0, 125, 25), checkWall);
    }
#endif
    #endregion


}