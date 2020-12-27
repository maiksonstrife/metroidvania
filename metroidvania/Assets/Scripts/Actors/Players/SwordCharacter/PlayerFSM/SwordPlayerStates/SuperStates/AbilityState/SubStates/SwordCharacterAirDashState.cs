using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterAirDashState : SwordCharacterAbilityState
{
    public bool CanAirDash { get; private set; }
    private float _lastAirDashTime;
    private bool _isHolding;
    private bool _dashInputStop;
    private Vector2 _airDashFaceDirection;
    private Vector2 _airDashDirectionInput;
    private Vector2 _lastAfterImagePos;

    public SwordCharacterAirDashState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string _animBoolName) : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        SwordCharacter.SetShockWave(true);
        CanAirDash = false; //As we enter we used our dash
        SwordCharacter.InputHandler.DashInputUsed(); //Let the input known
        _isHolding = true; 
        _airDashFaceDirection = Vector2.right * SwordCharacter.FacingDirection; //dash direction
        Time.timeScale = SwordCharacterData.HoldTimeScale; //Bullet Time
        startTime = Time.unscaledTime; //Since time is slowed down, startTime should be override
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isExitingState) return;

        SwordCharacter.Anim.SetFloat("yVelocity", SwordCharacter.CurrentVelocity.y);
        SwordCharacter.Anim.SetFloat("xVelocity", Mathf.Abs(SwordCharacter.CurrentVelocity.x));

        if (_isHolding)
        {
            _airDashDirectionInput = SwordCharacter.InputHandler.DashDirectionInput;
            _dashInputStop = SwordCharacter.InputHandler.DashInputStop;

            if(_airDashDirectionInput != Vector2.zero)
            {
                _airDashFaceDirection = _airDashDirectionInput;
                _airDashFaceDirection.Normalize();
            }

            if(_dashInputStop || Time.unscaledTime >= startTime + SwordCharacterData.AirDashMaxHoldTime)
            {
                _isHolding = false;
                SwordCharacter.SetShockWave(false);
                Time.timeScale = 1;
                startTime = Time.time;
                SwordCharacter.CheckIfShouldFlip(Mathf.RoundToInt(_airDashFaceDirection.x));
                SwordCharacter.RB.drag = SwordCharacterData.Drag;
                SwordCharacter.SetVelocityEightDirectional(SwordCharacterData.AirDashVelocity, _airDashFaceDirection);
                PlaceAfterImage();
            }
        }
        else
        {
            SwordCharacter.SetVelocityEightDirectional(SwordCharacterData.AirDashVelocity, _airDashFaceDirection);
            CheckIfShouldPlaceAfterImage();
            if (Time.time >= startTime + SwordCharacterData.AirDashTimeDistance)
            {
                SwordCharacter.RB.drag = 0;
                IsAbilityDone = true;
                _lastAirDashTime = Time.time;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        if (SwordCharacter.CurrentVelocity.y > 0)
        {
            SwordCharacter.SetVelocityY(SwordCharacter.CurrentVelocity.y * SwordCharacterData.DashEndYMultiplier);
        }
    }

    private void PlaceAfterImage()
    {
        AfterImagePool.Instance.GetFromPool();
        _lastAfterImagePos = SwordCharacter.transform.position;
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if(Vector2.Distance(SwordCharacter.transform.position, _lastAfterImagePos) >= SwordCharacterData.DistBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }

    public bool CheckIfCanDash()
    {
        return CanAirDash && Time.time >= _lastAirDashTime + SwordCharacterData.AirDashCooldown;
    }

    public void ResetCanDash() => CanAirDash = true;
}
