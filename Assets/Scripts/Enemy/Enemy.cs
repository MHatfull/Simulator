using System;
using UnityEngine;

[RequireComponent(typeof(EnemyNavigation))]
public abstract class Enemy : Character {

    public NavArea NavArea { get { return _enemyNavigation.NavArea; } set { _enemyNavigation.NavArea = value; } }

    private EnemyNavigation _enemyNavigation;

    public new void Awake()
    {
        base.Awake();
        _enemyNavigation = GetComponent<EnemyNavigation>();
    }

}
