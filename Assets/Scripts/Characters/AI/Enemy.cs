using Simulator.Abilities;
using Simulator.UI.Health;
using System;
using UnityEngine;

namespace Simulator.Characters.AI
{
    [RequireComponent(typeof(EnemyNavigation))]
    [RequireComponent(typeof(EnemyAbilityController))]
    [RequireComponent(typeof(HealthBar))]
    public abstract class Enemy : Character
    {
        public Character Hunting;

        public new void Awake()
        {
            base.Awake();
        }
    }
}