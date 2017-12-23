using UnityEngine;

[RequireComponent(typeof(HealthDisplay))]
[RequireComponent(typeof(AbilityController))]
public abstract class Character : MonoBehaviour {

    public delegate void OnDeathHandler();
    public event OnDeathHandler OnDeath;

    public delegate void OnHealthChangedHandler(float value);
    public event OnHealthChangedHandler OnHealthChanged;

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

    public void Awake()
    {
        _healthDisplay = GetComponent<HealthDisplay>();
    }

    public void Start()
    {
        _healthDisplay.MaxHealth = _maxHealth;
        CurrentHealth = MaxHealth;
    }

    public virtual void Die()
    {
        if (OnDeath != null) OnDeath();
    }

    public virtual void DealDamage(float damage, Transform source)
    {
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
