using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ToolsComponent : MonoBehaviour
{


    [Header("Use of tools Logic Objects")]
    [Tooltip("The inputs this component will receive")]
    [SerializeField] private PlayerInputManager _source;

    [SerializeField] private PlayerInventoryObject _playerInventory;

    [SerializeField] private Camera _toolRaycastOrigin;

    [SerializeField] private ToolBehaviour _currentTool;

    public UnityEvent onBranchCut;

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
        if (_currentTool)
        {
            if (_currentTool.Cut()) onBranchCut.Invoke();
        }
    }

    public void SwitchTool()
    {
        _toolIdx += 1;
        if (_toolIdx >= _playerInventory.toolList.Count) _toolIdx = 0;
        _currentTool = _playerInventory.toolList[_toolIdx];
        _currentTool.Initialize(_toolRaycastOrigin, _timer);
    }
}
