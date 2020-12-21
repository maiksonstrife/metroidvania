using UnityEngine;
using UnityEngine.InputSystem;

public class SwordCharacterInputHandler : MonoBehaviour
{
    private PlayerInput PlayerInput;
    private Camera cam;

    public Vector2 RawMovementInput { get; private set; }
    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }
    public int InputX { get; private set; }
    public int InputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GrabInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }

    [SerializeField]
    private float _inputHoldTime = 0.2f;
    private float _jumpInputStartTime;
    private float _DashInputStartTime;

    private void Start()
    {
        PlayerInput = GetComponent<PlayerInput>();
        cam = Camera.main;
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
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
        if (context.started) GrabInput = true;
        if (context.canceled) GrabInput = false;
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            _DashInputStartTime = Time.time;
        }

        else if (context.canceled)
        {
            DashInputStop = true;
        }
    }

    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();
        
        if(PlayerInput.currentControlScheme == "KeyBoard")
        {
            RawDashDirectionInput = cam.ScreenToWorldPoint( (Vector3) RawDashDirectionInput) - transform.position;
        }

        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }

    public void DashInputUsed() => DashInput = false;
    public void JumpButtonUsed() => JumpInput = false;
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= _jumpInputStartTime + _inputHoldTime)
        {
            JumpInput = false;
        }
    }
    private void CheckDashInputHoldTime()
    {
        if (Time.time >= _DashInputStartTime + _inputHoldTime)
        {
            DashInput = false;
        }
    }
}