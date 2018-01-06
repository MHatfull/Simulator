using System;
using UnityEngine;

[RequireComponent(typeof(EnemyNavigation))]
[RequireComponent(typeof(EnemyAbilityController))]
[RequireComponent(typeof(HealthBar))]
public abstract class Enemy : Character {
    public Character Hunting;

    public new void Awake()
    {
        base.Awake();
    }
}
