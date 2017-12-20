using UnityEngine;

[RequireComponent(typeof(EnemyNavigation))]
public abstract class Spawnable : MonoBehaviour {
    public delegate void OnDeathHandler();
    public event OnDeathHandler OnDeath;

    public NavArea NavArea { get { return _enemyNavigation.NavArea; } set { _enemyNavigation.NavArea = value; } }

    private EnemyNavigation _enemyNavigation;

    public void Awake()
    {
        _enemyNavigation = GetComponent<EnemyNavigation>();
    }
}
