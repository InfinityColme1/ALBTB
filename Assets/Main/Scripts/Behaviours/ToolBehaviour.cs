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

    [Tooltip("FOR DEBUG ONLY: Resets this tools resistance to max whenever is equiped")]
    [SerializeField] private bool _startWithMaxResistance = false;

    protected Camera _origin;
    private float _currentResistance;
    [SerializeField] private bool _canCut = true;
    private Timer _timer;


    private void Awake()
    {
        _currentResistance = _totalResistance;
        Debug.Log("START - " + _currentResistance);
    }

    public void Initialize(Camera origin, Timer timer)
    {
        _origin = origin;

        if (!_timer)
        {
            _timer = timer;
            _timer.onTimerStopped.AddListener(() => { if (_currentResistance > 0) _canCut = true; });
        }

        if (_currentResistance < 0 || 
            _currentResistance > _totalResistance || 
            (_currentResistance == 0) && _canCut ||
            _startWithMaxResistance)
        {
            _currentResistance = _totalResistance;
        }

        if (_currentResistance > 0) _canCut = true;

    }

    public bool Cut()
    {
        if (_canCut)
        {
            _timer.StartTimer(_coolDown);
            _canCut = false;

            return CastRay();
        }

        return false;
    }

    protected bool CastRay()
    {
        Ray ray = _origin.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _distance, 1 << 7))
        {
            HealthComponent branchHealth = hit.collider.GetComponent<HealthComponent>();
            if (branchHealth)
            {
                _currentResistance -= _resistanceDecrease;
                if (_currentResistance < 0) _canCut = false;

                return branchHealth.DoDamage(_damage, _currentResistance <= _totalResistance * _resistanceBoundary);
            }

        }

        return false;
    }
}
