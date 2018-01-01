using System;
using UnityEngine;

[RequireComponent(typeof(EnemyNavigation))]
public abstract class Enemy : Character {
    public Character Hunting;

    public new void Awake()
    {
        base.Awake();
    }
}
