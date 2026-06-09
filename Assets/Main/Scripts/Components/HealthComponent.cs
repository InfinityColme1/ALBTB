using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("Health component settings")]
    [Tooltip("The total health points of this component")]
    [SerializeField] private float _healthPoints;
    [Tooltip("The normal drop that this object might drop when health points run out")]
    [SerializeField] private InteractiveComponent _normalDrop;
    [Tooltip("The perfect drop that this object might drop when health points run out when doing perfect damage")]
    [SerializeField] private InteractiveComponent _perfectDrop;


    private float _currentHealthPoints;

    public float GetHealth() => _healthPoints;
    public void SetHealth(float points) => _healthPoints = points;


    private void Start()
    {
        _currentHealthPoints = _healthPoints;
    }

    public bool DoDamage(float damage, bool isPerfect = false)
    {
        _currentHealthPoints -= damage;
        if (_currentHealthPoints <= 0) {
            OnDestroyed(isPerfect);
            return true;
        }

        return false;
    }

    private void OnDestroyed(bool isPerfect)
    {
        InteractiveComponent drop = isPerfect ? _perfectDrop : _normalDrop;
        if (drop) Instantiate(drop);
        Destroy(this.gameObject);
    }
}
