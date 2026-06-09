using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInputManager : InputManager
{

    [Header("Mouse Cursor Settings")]
    [Tooltip("Determines if the cursor should be locked when this component loads")]
    public bool startWithLockedCursor = true; // TEMP: Esto quizás se quita si se gestiona en un manager superior
    [Tooltip("Indicates if the cursor is locked")]
    private bool _cursorLocked = true;
    [Tooltip("The current mouse sensitivty")]
    [SerializeField] private float mouseSensivity = 0.005f;

    [Tooltip("Indicates that the player tries to use a tool")]
    public bool cut;

    public UnityEvent onNextTool;


    private void Start()
    {
        if (startWithLockedCursor) setCursorStateLocked(startWithLockedCursor);
    }


    public void setCursorStateLocked(bool newState)
    {
        _cursorLocked = newState;
        Cursor.lockState = _cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }


    public void OnMove(InputValue value) => MoveInput(value.Get<Vector2>());
    public void OnLook(InputValue value) => LookInput(value.Get<Vector2>());
    public void OnSprint(InputValue value) => SprintInput(value.isPressed);
    public void OnInteract(InputValue value) => InteractInput();
    public void OnAttack(InputValue value) => OnCut(value.isPressed);
    public void OnNext(InputValue value) => onNextTool.Invoke();


    public override void MoveInput(Vector2 newMoveDirection) => move = newMoveDirection;
    public override void LookInput(Vector2 newLookDirection) => look = newLookDirection;
    public override void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }
    public override void InteractInput() => onInteract.Invoke();

    public override float GetYawFromLook()
    {
        return look.x * mouseSensivity;
    }
    public override float GetPitchFromLook()
    {
        return look.y * mouseSensivity;
    }

    public void OnCut(bool newState) => cut = newState;
}