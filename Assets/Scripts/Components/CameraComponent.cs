using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Windows;

public class CameraComponent : MonoBehaviour
{
    [Header("Input and Output")]
    [Tooltip("The inputs the camera will receive")]
    [SerializeField] private InputManager source;
    [Tooltip("The camera this component control")]
    [SerializeField] private CinemachineCamera cam;

    [Header("Camera Values")]
    [Tooltip("How far in degrees can you move the camera up")]
    public float topClamp = 90.0f;
    [Tooltip("How far in degrees can you move the camera down")]
    public float bottomClamp = -90.0f;


    private float _currentPitch;

    private void Awake()
    {
        source = this.gameObject.GetComponentInParent<InputManager>();
        cam = this.gameObject.GetComponentInChildren<CinemachineCamera>();
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
        _currentPitch -= source.GetPitchFromLook();
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
