using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InputManager: MonoBehaviour
{
    [Header("Input Values")]
    public Vector2 move;
    protected Vector2 look;
    public bool sprint;
    public UnityEvent onInteract;

    public abstract void MoveInput(Vector2 newMoveDirection);
    public abstract void LookInput(Vector2 newLookDirection);
    public Vector2 GetLook() { return look; }
    public abstract float GetYawFromLook();
    public abstract float GetPitchFromLook();
    public abstract void SprintInput(bool newSprintState);
    public abstract void InteractInput();
}
