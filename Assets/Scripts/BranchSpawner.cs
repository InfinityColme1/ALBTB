using UnityEngine;

public class BranchSpawner : MonoBehaviour
{
    private enum Direction { X, Y, Z}

    [Header("Spawn settings")]
    [Tooltip("The instance that will be spawned")]
    [SerializeField] private GameObject _instance;

    [Tooltip("How many instances will spawn.")]
    public float spawnNumber = -1;
    [Tooltip("The spawner will spawn instances until covering this distance. Ignores Spawn Number")]
    public float spawnDistance = -1;
    [Tooltip("The space between instances. This number should be a positive number")]
    public float spaceBetween = 0f;

    [Tooltip("The direction the spawner will follow.")]
    [SerializeField] private Direction _directionAxis = Direction.Z;
    [Tooltip("Determines if the spawn follows positive or negative direction")]
    [SerializeField] private bool _followsPositiveDirection = true;
    [Tooltip("Determines if the instances are spawned when this spawner starts.")]
    [SerializeField] private bool _spawnOnStart = false;

    [Header("Instance settings")]
    [Tooltip("The width of the instance. Adds to instance's prefab width")]
    [SerializeField] private float _width = 0f;
    [Tooltip("The height of the instance. Adds to instance's prefab height")]
    [SerializeField] private float _height = 0f;
    [Tooltip("The thickness of the instance. Adds to instance's prefab thickness")]
    [SerializeField] private float _thickness = 0f;


    private float _currentDistance = 0;
    private Vector3 _currentPosition;
    private float _instanceBulk;
    private Vector3 _instanceTransformOffset;
    private Vector3 _directionVector;
    private int _positiveModifier;

    void Start()
    {
        InitializeSpawner();
        if (_spawnOnStart) Spawn();
    }

    private void InitializeSpawner()
    {
        _currentPosition = transform.localPosition;
        _instanceTransformOffset = new Vector3(_width, _height, _thickness);
        _positiveModifier = _followsPositiveDirection ? 1 : -1;
        switch(_directionAxis)
        {
            case Direction.X:
                _instanceBulk = _instance.transform.localScale.x;
                _directionVector = Vector3.right * _positiveModifier;
                break;
            case Direction.Y:
                _instanceBulk = _instance.transform.localScale.y;
                _directionVector = Vector3.up * _positiveModifier;
                break;
            default:
                _instanceBulk = _instance.transform.localScale.z;
                _directionVector = Vector3.forward * _positiveModifier;
                break;
        }
    }

    public void Spawn()
    {
        if (spawnDistance >= 0) SpawnByDistance();
        else if (spawnNumber >= 0) SpawnByNumber();
    }

    private void SpawnByDistance()
    {
        while (_currentDistance < spawnDistance)
        {
            GameObject obj = Instantiate(_instance, _currentPosition, Quaternion.identity, this.transform);
            obj.transform.localScale += _instanceTransformOffset;
            UpdateDistances();
        }
    }

    private void SpawnByNumber()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            GameObject obj = Instantiate(_instance, _currentPosition, Quaternion.identity, this.transform);
            obj.transform.localScale += _instanceTransformOffset;
            UpdateDistances();
        }
    }

    private void UpdateDistances()
    {
        _currentDistance += (_instanceBulk + spaceBetween);
        _currentPosition += (_instanceBulk + spaceBetween) * _directionVector;
    }
}
