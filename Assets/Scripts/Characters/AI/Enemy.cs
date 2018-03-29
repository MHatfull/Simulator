using System;
using Underlunchers.Characters.AI.Navigation;
using UnityEngine;

namespace Underlunchers.Characters.AI
{
    [RequireComponent(typeof(EnemyNavigation))]
    public abstract class Enemy : Character
    {
        public Character Hunting;

        public new void Awake()
        {
            base.Awake();
        }
    }
}