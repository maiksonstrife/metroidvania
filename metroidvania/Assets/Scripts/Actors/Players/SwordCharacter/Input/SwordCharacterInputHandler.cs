using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordCharacterInputHandler : MonoBehaviour
{

    public Vector2 RawMovementInput { get; private set; }
    public int InputX { get; private set; }
    public int InputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GrabInput { get; private set; }

    [SerializeField]
    private float _inputHoldTime = 0.2f;
    private float _jumpInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context){
        RawMovementInput = context.ReadValue<Vector2>();
        
        if (Mathf.Abs(RawMovementInput.x) > 0.5f)
            InputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        else InputX = 0;

        if (Mathf.Abs(RawMovementInput.y) > 0.5f)
            InputY = (int)(RawMovementInput * Vector2.up).normalized.y;
        else InputY = 0;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            _jumpInputStartTime = Time.time;
        }
        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }

        if (context.canceled)
        {
            GrabInput = false;
        }
    }

    public void JumpButtonUsed() => JumpInput = false;
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= _jumpInputStartTime + _inputHoldTime)
        {
            JumpInput = false;
        }
    }
}