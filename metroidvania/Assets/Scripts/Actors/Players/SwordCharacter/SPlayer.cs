using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayer : MonoBehaviour
{
    #region State Variables
    public SPlayerIdleState IdleState { get; private set; }
    public SPlayerMoveState MoveState { get; private set; }
    public SPlayerStateMachine StateMachine { get; private set; }
    public SPlayerJumpState JumpState { get; private set; }
    public SPlayerAirState AirState { get; private set; }
    public SPlayerLandState LandState { get; private set; }
    public SPlayerWallSlideState WallSlideState { get; private set; }
    public SPlayerWallGrabState WallGrabState { get; private set; }
    public SPlayerWallClimbState WallClimbState { get; private set; }
    public SPlayerWallJumpState WallJumpState { get; private set; }
    public SPlayerLedgeClimbState LedgeCLimbState { get; private set; }
    public SPlayerAirDashState AirDashState { get; private set; }
    public SPlayerDashState DashState { get; private set; }

    [SerializeField]
    private SPlayerData _SPlayerData;
    #endregion

    #region Components
    public SwordCharacterInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }
    #endregion

    #region Check Transforms
    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private Transform _wallCheck;    
    [SerializeField]
    private Transform _wallCheckAbove;
    [SerializeField]
    private Transform _shockWave;
    #endregion

    #region Player Variables
    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 _workSpace;
    #endregion

    #region Debugging
    private string _previousState;
    private bool _canLog;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new SPlayerStateMachine();
        InputHandler = GetComponent<SwordCharacterInputHandler>();
        IdleState = new SPlayerIdleState(this, StateMachine, _SPlayerData, "idle");
        MoveState = new SPlayerMoveState(this, StateMachine, _SPlayerData, "move");
        JumpState = new SPlayerJumpState(this, StateMachine, _SPlayerData, "inAir");
        AirState = new SPlayerAirState(this, StateMachine, _SPlayerData, "inAir");
        LandState = new SPlayerLandState(this, StateMachine, _SPlayerData, "land");
        WallSlideState = new SPlayerWallSlideState(this, StateMachine, _SPlayerData, "wallSlide");
        WallGrabState = new SPlayerWallGrabState(this, StateMachine, _SPlayerData, "wallGrab");
        WallClimbState = new SPlayerWallClimbState(this, StateMachine, _SPlayerData, "wallClimb");
        WallJumpState = new SPlayerWallJumpState(this, StateMachine, _SPlayerData, "inAir");
        LedgeCLimbState = new SPlayerLedgeClimbState(this, StateMachine, _SPlayerData, "ledgeClimb");
        AirDashState = new SPlayerAirDashState(this, StateMachine, _SPlayerData, "inAir");
        DashState = new SPlayerDashState(this, StateMachine, _SPlayerData, "groundDash");
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
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
        //Debug.DrawRay(_wallCheck.position, Vector2.right * FacingDirection * _characterData.WallCheckDistance, Color.cyan);
    }

    private void FixedUpdate() => StateMachine.CurrentState.PhysicsUpdate();
    #endregion

    #region Set Functions
    public void SetShockWave(bool isActive)
    {
        foreach (Transform child in _shockWave)
        {
            child.gameObject.SetActive(isActive);
        }
    }

    public void SetVelocityAngular(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        RB.velocity = _workSpace;
        CurrentVelocity = _workSpace;
    }

    public void SetVelocityEightDirectional(float velocity, Vector2 direction)
    {
        _workSpace = direction * velocity;
        RB.velocity = _workSpace;
        CurrentVelocity = _workSpace;
    }

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

    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    #endregion

    #region Check Functions
    public bool CheckIfTouchingGround()
    {
        if (Physics2D.OverlapCircle(_groundCheck.position, _SPlayerData.GroundCheckRadius, _SPlayerData.WhatIsGround)) return true;
        if (Physics2D.OverlapCircle(_groundCheck.position, _SPlayerData.GroundCheckRadius, _SPlayerData.WhatIsGrabbable)) return true;
        else return false;
    }

    public void CheckIfShouldFlip(int XInput)
    {
        if (XInput != 0 && XInput != FacingDirection) Flip();
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _SPlayerData.WallCheckDistance, _SPlayerData.WhatIsGround);
    }

    public bool CheckIfTouchingWallAbove()
    {
        return Physics2D.Raycast(_wallCheckAbove.position, Vector2.right * FacingDirection, _SPlayerData.WallCheckDistance, _SPlayerData.WhatIsGround);
    }

    public bool CheckIfTouchingGrabbableAbove()
    {
        return Physics2D.Raycast(_wallCheckAbove.position, Vector2.right * FacingDirection, _SPlayerData.WallCheckDistance, _SPlayerData.WhatIsGrabbable);
    }

    public bool CheckIfTouchingWallBack()
    {
        if (Physics2D.Raycast(_wallCheck.position, Vector2.right * -FacingDirection, _SPlayerData.WallCheckDistance, _SPlayerData.WhatIsGround)) return true;
        if (Physics2D.Raycast(_wallCheck.position, Vector2.right * -FacingDirection, _SPlayerData.WallCheckDistance, _SPlayerData.WhatIsGrabbable)) return true;
        else return false;
    }

    public bool CheckIfIsGrabbable()
    {
        return Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _SPlayerData.WallCheckDistance, _SPlayerData.WhatIsGrabbable);
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

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _SPlayerData.WallCheckDistance, _SPlayerData.WhatIsGround);
        float xDist = xHit.distance;
        _workSpace.Set((xDist + 0.015f) * FacingDirection, 0f);

        RaycastHit2D yHit = Physics2D.Raycast(_wallCheckAbove.position + (Vector3) (_workSpace), Vector2.down, _wallCheckAbove.position.y - _wallCheck.position.y + 0.015f, _SPlayerData.WhatIsGround);
        float yDist = yHit.distance;

        _workSpace.Set(_wallCheck.position.x + (xDist * FacingDirection), _wallCheckAbove.position.y - yDist);
        return _workSpace;
    }
    #endregion

    #region Gizmos
#if UNITY_EDITOR
    void OnGUI()
    {
        string currentState = StateMachine.CurrentState.ToString();
        GUI.Box(new Rect(0, 0, 200, 25), currentState);

        if (GUI.Button(new Rect(0, Screen.height - 25, 200, 25), $"Create States Log        {_canLog}")) _canLog = !_canLog;

        if (_previousState != currentState && _canLog) Debug.Log(currentState);

        string checkGrabbable = "Grabbable : " + CheckIfIsGrabbable();
        GUI.Box(new Rect(205, 0, 125, 25), checkGrabbable);

        string checkGround = "Ground : " + CheckIfTouchingGround();
        GUI.Box(new Rect(335, 0, 125, 25), checkGround);

        string checkWall = "Wall : " + CheckIfTouchingWall();
        GUI.Box(new Rect(465, 0, 125, 25), checkWall);

        string checkWallAbove = "Wall Above: " + CheckIfTouchingWallAbove();
        GUI.Box(new Rect(595, 0, 125, 25), checkWallAbove);

        string checkWallBack = "Wall Back: " + CheckIfTouchingWallBack();
        GUI.Box(new Rect(730, 0, 125, 25), checkWallBack);

        _previousState = currentState;
    }
#endif
    #endregion

}