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
    public SwordCharacterWallJumpState WallJumpState { get; private set; }
    public SwordCharacterLedgeClimbState LedgeCLimbState { get; private set; }
    public SwordCharacterAirDashState DashState { get; private set; }

    [SerializeField]
    private SwordCharacterData _characterData;
    #endregion

    #region Components
    public SwordCharacterInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }
    [SerializeField]
    private GameObject ShockWave;
    #endregion

    #region Check Transforms
    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private Transform _wallCheck;    
    [SerializeField]
    private Transform _wallCheckAbove;
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
        WallJumpState = new SwordCharacterWallJumpState(this, StateMachine, _characterData, "inAir");
        LedgeCLimbState = new SwordCharacterLedgeClimbState(this, StateMachine, _characterData, "ledgeClimb");
        DashState = new SwordCharacterAirDashState(this, StateMachine, _characterData, "inAir");
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
        ShockWave.SetActive(isActive);
        foreach (Transform child in ShockWave.transform)
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
        return Physics2D.OverlapCircle(_groundCheck.position, _characterData.GroundCheckRadius, _characterData.WhatIsGround);
    }

    public void CheckIfShouldFlip(int XInput)
    {
        if (XInput != 0 && XInput != FacingDirection) Flip();
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _characterData.WallCheckDistance, _characterData.WhatIsGround);
    }

    public bool CheckIfTouchingWallAbove()
    {
        return Physics2D.Raycast(_wallCheckAbove.position, Vector2.right * FacingDirection, _characterData.WallCheckDistance, _characterData.WhatIsGround);
    }

    public bool CheckIfTouchingGrabbableAbove()
    {
        return Physics2D.Raycast(_wallCheckAbove.position, Vector2.right * FacingDirection, _characterData.WallCheckDistance, _characterData.WhatIsGrabbable);
    }

    public bool CheckIfTouchingWallBack()
    {
        if (Physics2D.Raycast(_wallCheck.position, Vector2.right * -FacingDirection, _characterData.WallCheckDistance, _characterData.WhatIsGround)) return true;
        if (Physics2D.Raycast(_wallCheck.position, Vector2.right * -FacingDirection, _characterData.WallCheckDistance, _characterData.WhatIsGrabbable)) return true;
        else return false;
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

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _characterData.WallCheckDistance, _characterData.WhatIsGround);
        float xDist = xHit.distance;
        _workSpace.Set(xDist * FacingDirection, 0f);

        RaycastHit2D yHit = Physics2D.Raycast(_wallCheckAbove.position + (Vector3) (_workSpace), Vector2.down, _wallCheckAbove.position.y - _wallCheck.position.y, _characterData.WhatIsGround);
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