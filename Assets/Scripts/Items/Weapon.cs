using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simulator.Items
{
    public class Weapon : Equipment
    {
        Animator _animator;

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }
    }
}