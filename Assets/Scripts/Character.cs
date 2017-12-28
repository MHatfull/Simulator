using UnityEngine;

[RequireComponent(typeof(HealthDisplay))]
[RequireComponent(typeof(AbilityController))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour {

    public delegate void OnDeathHandler();
    public event OnDeathHandler OnDeath;

    private Animator _animator;
    private HealthDisplay _healthDisplay;

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

    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _healthDisplay = GetComponent<HealthDisplay>();
    }

    public void Start()
    {
        _healthDisplay.MaxHealth = _maxHealth;
        CurrentHealth = MaxHealth;
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
