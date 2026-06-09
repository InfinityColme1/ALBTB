using UnityEngine;
using UnityEngine.Events;

public class StreakComponent : MonoBehaviour
{
    [Header("Streak System Logic Objects")]
    [SerializeField] private InventoryComponent _inventoryComponent;
    [SerializeField] private ToolsComponent _toolsComponent;

    [Header("Streak System Settings")]
    [SerializeField] private int _pointBoundary;
    [SerializeField] private int _pointsPerBoundary = 1;
    [SerializeField] private int _maxPointsToExtra = 3;
    [SerializeField] private int _maxPointsToFinalize= 5;
    [SerializeField] private float _streakFactor;
    [SerializeField] private float _streakTime;

    public UnityEvent onExtraStreakPoint;
    public UnityEvent onFinalizeStreakPoint;

    private const float BFACTOR_BASE = 1.0f;
    private const float BFACTOR_MULT = 1.0f;

    private int _branchProgress;
    private int _storedStreak;
    private int _storedExtra;
    private int _currentBoundary;
    private Timer _timer;


    private void Awake()
    {
        _timer = gameObject.GetComponent<Timer>();
    }


    public void Start()
    {
        _toolsComponent.onBranchCut.AddListener(OnBranchCut);
        _timer.onTimerStopped.AddListener(() => StopStreak());
    }

    public void StartStreak()
    {
        _currentBoundary = _pointBoundary;
        _branchProgress = 0;
        _storedStreak = 0;
        _storedExtra = 0;

        Debug.Log("START STREAK - " + _currentBoundary + " - " + _branchProgress);
    }

    public void StopStreak(bool isFinalized = false)
    {
        _branchProgress = 0;
        _currentBoundary = _pointBoundary;

        int total = (isFinalized) ? _storedStreak : _storedStreak + _storedExtra;
        _inventoryComponent.AddCurrency(CurrencyType.STREAK, total);

        _storedStreak = 0;
        _storedExtra = 0;

        Debug.Log("STREAK STOPPED");
    }

    private void OnBranchCut()
    {
        if (_timer.isStoped()) StartStreak();

        _timer.StartTimer(_streakTime);

        _branchProgress++;
        Debug.Log("Progress: " + _branchProgress + " / " + _currentBoundary);
        if (_branchProgress >= _currentBoundary)
        {

            if (_storedStreak < _maxPointsToExtra)
            {
                _storedStreak += _pointsPerBoundary;
                Debug.Log("STREAK POINTS:  " + _storedStreak + " / " + _maxPointsToExtra);
            }
            else if ((_storedExtra + _storedStreak) <= _maxPointsToFinalize)
            {
                _storedExtra += _pointsPerBoundary;
                Debug.Log("STREAK EXTRA TOTAL: " + (_storedExtra + _storedStreak) + " / " + _maxPointsToFinalize);

                onExtraStreakPoint.Invoke();
            }
            else
            {
                StopStreak(isFinalized: true);
                onFinalizeStreakPoint.Invoke();
            }

            UpdatePointBoundary();
            _branchProgress = 0;
        }

    }

    private void UpdatePointBoundary()
    {
        float bFactor = BFACTOR_BASE + (_storedStreak * BFACTOR_MULT);

        float streakModifier = _streakFactor * bFactor;

        _currentBoundary = Mathf.RoundToInt(1.0f / streakModifier);

        Debug.Log("NEW BOUNDARY: " + _currentBoundary);
    }
}
