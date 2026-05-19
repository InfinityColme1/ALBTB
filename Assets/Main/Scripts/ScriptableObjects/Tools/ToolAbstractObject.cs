using Unity.Cinemachine;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolObject", menuName = "Scriptable Objects/ToolObject")]
public abstract class ToolAbstractObject : ScriptableObject
{
    [Header("Tool Raycast settings")]
    public float distance;
    public float coolDown;
    public float damage;

    protected CinemachineCamera _cam;

    public void Initialize(CinemachineCamera cam)
    {
        _cam = cam;
    }

    public abstract void Cut();
}
