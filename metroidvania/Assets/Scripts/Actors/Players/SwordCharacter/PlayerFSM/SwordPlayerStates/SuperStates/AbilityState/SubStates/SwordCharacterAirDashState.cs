using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterAirDashState : SwordCharacterAbilityState
{
    public bool CanAirDash { get; private set; }
    private float _lastAirDashTime;
    private bool _isHolding;
    private bool _dashInputStop;
    private Vector2 _airDashDirection;
    private Vector2 _airDashDirectionInput;
    private Vector2 _lastAfterImagePos;

    public SwordCharacterAirDashState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string _animBoolName) : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        CanAirDash = false; //As we enter we used our dash
        SwordCharacter.InputHandler.DashInputUsed(); //Let the input known
        _isHolding = true; //The current state
        _airDashDirection = Vector2.right * SwordCharacter.FacingDirection; //dash direction
        Time.timeScale = SwordCharacterData.HoldTimeScale; //Bullet Time
        startTime = Time.unscaledTime; //Since time is slowed down, startTime should be override
    }

    public override void Exit()
    {
        base.Exit();
        if(SwordCharacter.CurrentVelocity.y > 0)
        {
            SwordCharacter.SetVelocityY(SwordCharacter.CurrentVelocity.y * SwordCharacterData.DashEndYMultiplier);
        }
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
                _airDashDirection = _airDashDirectionInput;
                _airDashDirection.Normalize();
            }

            if(_dashInputStop || Time.unscaledTime >= startTime + SwordCharacterData.AirDashMaxHoldTime)
            {
                _isHolding = false;
                Time.timeScale = 1;
                startTime = Time.time;
                SwordCharacter.CheckIfShouldFlip(Mathf.RoundToInt(_airDashDirection.x));
                SwordCharacter.RB.drag = SwordCharacterData.Drag;
                SwordCharacter.SetVelocityEightDirectional(SwordCharacterData.AirDashVelocity, _airDashDirection);
                PlaceAfterImage();
            }
        }
        else
        {
            SwordCharacter.SetVelocityEightDirectional(SwordCharacterData.AirDashVelocity, _airDashDirection);
            CheckIfShouldPlaceAfterImage();
            if (Time.time >= startTime + SwordCharacterData.AirDashTimeDistance)
            {
                SwordCharacter.RB.drag = 0;
                IsAbilityDone = true;
                _lastAirDashTime = Time.time;
            }
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
            Debug.Log("is reachiing here ?");
            PlaceAfterImage();
        }
    }

    public bool CheckIfCanDash()
    {
        return CanAirDash && Time.time >= _lastAirDashTime + SwordCharacterData.AirDashCooldown;
    }

    public void ResetCanDash() => CanAirDash = true;

}
