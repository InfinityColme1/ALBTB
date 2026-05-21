using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class ToolsComponent : MonoBehaviour
{


    [Header("Use of tools Logic Objects")]
    [Tooltip("The inputs this component will receive")]
    [SerializeField] private PlayerInputManager _source;

    [SerializeField] private PlayerInventoryObject _playerInventory;

    [SerializeField] private Camera _toolRaycastOrigin;

    [SerializeField] private ToolBehaviour _currentTool; // TEMP: Serialización temporal, debería ser 100% priv

    private void Start()
    {
        //TEMP
        SwitchTool();
    }

    private void Update()
    {
        if (_source.cut) Cut();
    }

    private void Cut() => _currentTool.Cut();

    public void SwitchTool()
    {
        //TEMP
        _currentTool.Initialize(_toolRaycastOrigin);
    }
}
