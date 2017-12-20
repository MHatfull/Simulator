using System;
using UnityEngine;

[RequireComponent(typeof(EnemyNavigation))]
public abstract class Enemy : MonoBehaviour {

    public delegate void OnDeathHandler();
    public event OnDeathHandler OnDeath;

    public NavArea NavArea { get { return _enemyNavigation.NavArea; } set { _enemyNavigation.NavArea = value; } }
    public abstract float Health { get; protected set; }


    private EnemyNavigation _enemyNavigation;

    public void Awake()
    {
        _enemyNavigation = GetComponent<EnemyNavigation>();
    }

    internal void DealDamage(float damage)
    {
        Debug.Log("ouch, hit for " + damage);
        Health -= damage;
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if(Health <= 0)
        {
            if (OnDeath != null) OnDeath();
            Destroy(gameObject);
        }
    }
}
