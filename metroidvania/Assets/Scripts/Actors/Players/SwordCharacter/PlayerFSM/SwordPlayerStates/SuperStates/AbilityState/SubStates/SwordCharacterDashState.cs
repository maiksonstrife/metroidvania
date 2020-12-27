using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterDashState : SwordCharacterAbilityState
{
    public bool CanDash { get; private set; }
    public bool _isHolding;
    private Vector2 _lastAfterImagePos;
    private bool _dashInputStop;
    private float _lastDashTime;

    public SwordCharacterDashState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string _animBoolName) : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SwordCharacter.InputHandler.DashInputUsed();
        CanDash = false;
        _isHolding = true;
        PlaceAfterImage();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isExitingState) return;

        if (_jumpInput && _isHolding && IsGrounded)
        {
            SwordCharacter.RB.drag = 0;
            SwordCharacter.SetVelocityAngular(SwordCharacterData.DashJumpVelocity, SwordCharacterData.DashJumpAngle, SwordCharacter.FacingDirection);
        }

        if (_isHolding && IsGrounded)
        {
            _dashInputStop = SwordCharacter.InputHandler.DashInputStop;
            if (_dashInputStop) _isHolding = false;
            SwordCharacter.RB.drag = SwordCharacterData.Drag;
            SwordCharacter.CheckIfShouldFlip(XInput);
            SwordCharacter.SetVelocityX(SwordCharacterData.DashVelocity * SwordCharacter.FacingDirection);
        }

        if (!_isHolding || !IsGrounded || Time.time >= startTime + SwordCharacterData.DashTimeDistance)
        {
            SwordCharacter.RB.drag = 0;
            IsAbilityDone = true;
            _lastDashTime = Time.time;
        }
    }

    public override void Exit()
    {
        base.Exit();
        if (!IsGrounded) SwordCharacter.JumpState.DecreaseAmountOfJumpsLeft();

    }

    private void PlaceAfterImage()
    {
        AfterImagePool.Instance.GetFromPool();
        _lastAfterImagePos = SwordCharacter.transform.position;
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(SwordCharacter.transform.position, _lastAfterImagePos) >= SwordCharacterData.DashDistBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= _lastDashTime + SwordCharacterData.DashCooldown;
    }

    public void ResetCanDash() => CanDash = true;
}
