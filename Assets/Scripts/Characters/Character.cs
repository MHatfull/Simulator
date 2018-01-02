﻿using System;
using UnityEngine;

[RequireComponent(typeof(HealthDisplay))]
[RequireComponent(typeof(AbilityController))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour {

    [SerializeField] Sprite _miniMapTexture;

    public delegate void OnDeathHandler();
    public event OnDeathHandler OnDeath;

    private Animator _animator;
    private HealthDisplay _healthDisplay;

    public abstract void PlayWeaponAttackAnimation();

    public float MaxHealth
    {
        get { return _maxHealth; }
        protected set { _maxHealth = value; _healthDisplay.MaxHealth = value; }
    }
    [SerializeField] protected float _maxHealth;

    public float CurrentHealth
    {
        get { return _currentHealth; }
        protected set { _currentHealth = value; _healthDisplay.CurrentHealth = value; }
    }
    protected float _currentHealth;

    public abstract Vector3 FoculPoint { get; }

    internal abstract float DamageBonus();

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _healthDisplay = GetComponent<HealthDisplay>();
    }

    public void Start()
    {
        _healthDisplay.MaxHealth = _maxHealth;
        CurrentHealth = MaxHealth;
        var minimapIcon = new GameObject();
        minimapIcon.transform.parent = transform;
        minimapIcon.transform.eulerAngles = new Vector3(90, 0, 0);
        minimapIcon.transform.localPosition = new Vector3(0, 10, 0);
        minimapIcon.layer = LayerMask.NameToLayer("MiniMap");
        var minimapSprite = minimapIcon.AddComponent<SpriteRenderer>();
        minimapSprite.sprite = _miniMapTexture;
    }

    public abstract Vector3 AimDirection();

    public virtual void Die()
    {
        if (OnDeath != null) OnDeath();
    }

    public virtual void DealDamage(float damage, Character source)
    {
        _animator.SetTrigger("TakeDamage");
        CurrentHealth -= damage;
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (CurrentHealth <= 0)
        {
            Die();
            Destroy(gameObject);
        }
    }
}