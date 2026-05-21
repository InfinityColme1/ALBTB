using System.Collections;
using UnityEngine;


public class ToolBehaviour : MonoBehaviour
{
    [Header("Tool Raycast settings")]
    [Tooltip("The distance this tool will be able to cut")]
    [SerializeField] private float _distance;
    [Tooltip("Time between cuts")]
    [SerializeField] private float _coolDown;
    [Tooltip("The damage dealth by each cut")]
    [SerializeField] private float _damage;
    [Tooltip("The total resistance of the tool")]
    [SerializeField] private float _totalResistance;
    [Tooltip("The resistance decrease by each hit")]
    [SerializeField] private float _resistanceDecrease;
    [Tooltip("The resistance percentage that must be left for hits to be perfect")]
    [Range(0f, 1f)]
    [SerializeField] private float _resistanceBoundary;

    protected Camera _origin;
    private float _currentResistance;
    [SerializeField] private bool _canCut = true;
    private Timer _timer;


    private void Awake()
    {
        _currentResistance = _totalResistance;
        _timer = gameObject.GetComponent<Timer>();
    }

    private void Start()
    {
        _timer.onTimerStopped.AddListener(() => _canCut = true);
    }

    public void Initialize(Camera origin)
    {
        _origin = origin;
    }

    public void Cut()
    {
        if (_canCut)
        {
            CastRay();
            _timer.StartTimer(_coolDown);
            _canCut = false;
        }
    }

    protected void CastRay()
    {
        Ray ray = _origin.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _distance, 1 << 7))
        {
            HealthComponent branchHealth = hit.collider.GetComponent<HealthComponent>();
            if (branchHealth)
            {
                branchHealth.DoDamage(_damage, _currentResistance <= _totalResistance * _resistanceBoundary);
                _currentResistance -= _resistanceDecrease;
                Debug.Log("RESISTANCE: " + _currentResistance);
            }

        }
    }
}
