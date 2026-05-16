using UnityEngine;
using UnityEngine.Windows;

public class MovementComponent : MonoBehaviour
{
    [Header("Movement Logic Objects")]
    private InputManager _source;
    private CharacterController _controller;

    [Header("Movement Values")]
    [SerializeField] private float defaultSpeed = 5.0f;
    [SerializeField] private float sprintSpeed = 10.0f;


    private float _currentSpeed;
    private float _currentYaw;

    private void Awake()
    {
        _source = GetComponent<InputManager>();
        _controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        Move();
    }

    private void LateUpdate()
    {
        Turn();
    }

    void Move()
    {
        float targetSpeed = _source.sprint ? sprintSpeed : defaultSpeed;

        if (_source.move == Vector2.zero) targetSpeed = 0.0f;

        _currentSpeed = targetSpeed;

        Vector3 inputDirection = new Vector3(_source.move.x, 0.0f, _source.move.y).normalized;

        if (_source.move != Vector2.zero)
        {
            inputDirection = transform.right * _source.move.x + transform.forward * _source.move.y;
        }

        _controller.Move(inputDirection.normalized * (_currentSpeed * Time.deltaTime));
    }

    void Turn()
    {
        _currentYaw += _source.GetYawFromLook();
        transform.rotation = Quaternion.Euler(0.0f, _currentYaw, 0.0f);
    }
}
