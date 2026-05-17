using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("Health component settings")]
    [Tooltip("The total health points of this component")]
    [SerializeField] private float _healthPoints;
    [Tooltip("The drops that this object might drop when health points run out")]
    [SerializeField] private List<GameObject> _drops; //TODO: Esto quizas deberia ser solo collectables
 

    private float _currentHealthPoints;

    public float GetHealth() => _healthPoints;
    public void SetHealth(float points) => _healthPoints = points;


    private void Start()
    {
        _currentHealthPoints = _healthPoints;
        DoDamage(_currentHealthPoints);
    }

    public void DoDamage(float damage)
    {
        _currentHealthPoints -= damage;
        if (_currentHealthPoints <= 0) OnDestroyed();
    }

    private void OnDestroyed()
    {
        SpawnDrops();
        Destroy(this.gameObject);
    }

    private void SpawnDrops()
    {
        if (_drops.Count > 0)
        {
            GameObject drop = _drops[Random.Range(0, _drops.Count-1)];
            Instantiate(drop, this.gameObject.transform.position, Quaternion.identity);
        }
    }
}
