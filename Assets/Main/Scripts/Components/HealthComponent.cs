using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("Health component settings")]
    [Tooltip("The total health points of this component")]
    [SerializeField] private float _healthPoints;
    [Tooltip("The normal drop that this object might drop when health points run out")]
    [SerializeField] private CollectableObject _normalDrop;
    [Tooltip("The perfect drop that this object might drop when health points run out when doing perfect damage")]
    [SerializeField] private CollectableObject _perfectDrop;


    private float _currentHealthPoints;

    public float GetHealth() => _healthPoints;
    public void SetHealth(float points) => _healthPoints = points;


    private void Start()
    {
        _currentHealthPoints = _healthPoints;
    }

    public void DoDamage(float damage, bool isPerfect = false)
    {
        _currentHealthPoints -= damage;
        if (_currentHealthPoints <= 0) OnDestroyed(isPerfect);
    }

    private void OnDestroyed(bool isPerfect)
    {
        CollectableObject drop = isPerfect ? _perfectDrop : _normalDrop;
        Instantiate(drop);
        Destroy(this.gameObject);
    }
}
