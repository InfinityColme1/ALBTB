using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Windows;

public class CameraComponent : MonoBehaviour
{
    [Header("Input and Output")]
    [Tooltip("The inputs this component will receive")]
    [SerializeField] private InputManager _source;
    [Tooltip("The camera this component control")]
    [SerializeField] private CinemachineCamera _cam;

    [Header("Camera Values")]
    [Tooltip("How far in degrees can you move the camera up")]
    public float topClamp = 90.0f;
    [Tooltip("How far in degrees can you move the camera down")]
    public float bottomClamp = -90.0f;


    private float _currentPitch;

    private void Awake()
    {
        _source = this.gameObject.GetComponentInParent<InputManager>();
        if (!_source) Debug.LogError("This component needs an InputManager instance attached in this GameObject or in the parent");
        _cam = this.gameObject.GetComponentInChildren<CinemachineCamera>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        CameraRotation();
    }

    void CameraRotation()
    {
        _currentPitch -= _source.GetPitchFromLook();
        _currentPitch = ClampAngle(_currentPitch, bottomClamp, topClamp);

        transform.localRotation = Quaternion.Euler(_currentPitch, 0.0f, 0.0f);
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
