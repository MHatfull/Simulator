using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment {
    Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
    }
}
