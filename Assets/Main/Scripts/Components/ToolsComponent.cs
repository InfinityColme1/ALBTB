using Unity.Cinemachine;
using UnityEngine;

public class ToolsComponent : MonoBehaviour
{


    [Header("Use of tools Logic Objects")]
    [Tooltip("The inputs this component will receive")]
    [SerializeField] private PlayerInputManager _source;

    [SerializeField] private PlayerInventoryObject _playerInventory;

    private CinemachineCamera _cam;
    [SerializeField] private ToolAbstractObject _currentTool; // TEMP: Serialización temporal, debería ser 100% priv
    private int _toolIdx = 0;

    private void Awake()
    {
        _cam = GetComponent<CinemachineCamera>();
    }

    private void Start()
    {
        _source.onCut.AddListener(Cut);
    }

    private void Cut() => _currentTool.Cut();

    public void SwitchTool()
    {
        _currentTool.Initialize(_cam);
    }
}
