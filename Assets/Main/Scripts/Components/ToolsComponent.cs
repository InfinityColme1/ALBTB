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

    [SerializeField] private ToolBehaviour _currentTool;

    private int _toolIdx = 0;
    private Timer _timer;

    private void Awake()
    {
        _timer = gameObject.GetComponent<Timer>();
    }

    private void Start()
    {
        _source.onNextTool.AddListener(SwitchTool);
        if (_currentTool) _currentTool.Initialize(_toolRaycastOrigin, _timer);
    }

    private void Update()
    {
        if (_source.cut) Cut();
    }

    private void Cut()
    {
        if (_currentTool) _currentTool.Cut();
    }

    public void SwitchTool()
    {
        _toolIdx = (_toolIdx++) % _playerInventory.toolList.Count;
        _currentTool = _playerInventory.toolList[_toolIdx];
        _currentTool.Initialize(_toolRaycastOrigin, _timer);
    }
}
