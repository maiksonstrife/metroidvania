using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerLedgeClimbState : SwordCharaterState
{
    private Vector2 _detectedPos;
    private Vector2 _cornerPos;
    private Vector2 _startPos;
    private Vector2 _stopPos;
    private bool _isHanging;
    private bool _isClimbing;
    private int _xInput;
    private int _yInput;
    private bool _jumpInput;
    private float _timer;
    private float _inputDelay = 0.2f;

    public SPlayerLedgeClimbState(SPlayer swordCharacter, SPlayerStateMachine statemachine, SPlayerData swordCharacterData, string animBoolName) : base(swordCharacter, statemachine, swordCharacterData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        SwordCharacter.Anim.SetBool("ledgeClimbUp", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        _isHanging = true;
        _timer = Time.time;
    }

    public override void Enter()
    {
        base.Enter();
        SwordCharacter.SetVelocityZero();
        SwordCharacter.transform.position = _detectedPos;
        _cornerPos = SwordCharacter.DetermineCornerPosition();

        _startPos.Set(_cornerPos.x - (SwordCharacter.FacingDirection * SwordCharacterData.StartOffset.x), _cornerPos.y - SwordCharacterData.StartOffset.y);
        _stopPos.Set(_cornerPos.x + (SwordCharacter.FacingDirection * SwordCharacterData.StopOffset.x), _cornerPos.y + SwordCharacterData.StartOffset.y);
        SwordCharacter.transform.position = _startPos;
    }

    public override void Exit()
    {
        base.Exit();
        _isHanging = false;
        if (_isClimbing)
        {
            SwordCharacter.transform.position = _stopPos;
            _isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.IdleState);
        }
        else
        {
            _xInput = SwordCharacter.InputHandler.InputX;
            _yInput = SwordCharacter.InputHandler.InputY;
            _jumpInput = SwordCharacter.InputHandler.JumpInput;
            SwordCharacter.SetVelocityZero();

            if (!_isClimbing) SwordCharacter.transform.position = _startPos;
            else SwordCharacter.transform.position = Vector3.MoveTowards(SwordCharacter.transform.position, _stopPos, SwordCharacterData.OffsetTransition * Time.deltaTime);

            if (_xInput == SwordCharacter.FacingDirection && _isHanging && !_isClimbing && Time.time > _timer + _inputDelay)
            {
                _isClimbing = true;
                SwordCharacter.Anim.SetBool("ledgeClimbUp", true);
            }
            else if (_yInput == -1 && _isHanging && !_isClimbing)
            {
                SwordCharaterStateMachine.ChangeState(SwordCharacter.AirState);
            }
            //else if(_jumpInput && !_isClimbing && Time.time > _timer + _inputDelay)
            //{
            //    SwordCharacter.WallJumpState.CheckWallJumpDirection(true);
            //    SwordCharaterStateMachine.ChangeState(SwordCharacter.WallJumpState);
            //}
        }
    }

    public void SetDetectedPosition(Vector2 pos) => _detectedPos = pos;
}
