using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : InputManager
{

    [Header("Mouse Cursor Settings")]
    public bool startWithLockedCursor = true; // TEMP: Esto quizás se quita si se gestiona en un manager superior
    private bool cursorLocked = true;
    [SerializeField] private float mouseSensivity = 0.01f;


    private void Start()
    {
        if (startWithLockedCursor) setCursorStateLocked(startWithLockedCursor);
    }


    public void setCursorStateLocked(bool newState)
    {
        cursorLocked = newState;
        Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }


    public void OnMove(InputValue value) => MoveInput(value.Get<Vector2>());
    public void OnLook(InputValue value) => LookInput(value.Get<Vector2>());

    public void OnSprint(InputValue value) => SprintInput(value.isPressed);
    public void OnInteract(InputValue value) => InteractInput(value.isPressed);


    public override void MoveInput(Vector2 newMoveDirection) => move = newMoveDirection;
    public override void LookInput(Vector2 newLookDirection) => look = newLookDirection;
    public override void SprintInput(bool newSprintState) => sprint = newSprintState;
    public override void InteractInput(bool newInteractState) => interact = newInteractState;

    public override float GetYawFromLook()
    {
        return look.x * mouseSensivity;
    }
}
