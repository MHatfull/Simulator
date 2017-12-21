using UnityEngine;

[RequireComponent(typeof(HealthBar))]
public abstract class Character : MonoBehaviour {

    public delegate void OnDeathHandler();
    public event OnDeathHandler OnDeath;

    public delegate void OnHealthChangedHandler(float value);
    public event OnHealthChangedHandler OnHealthChanged;

    private HealthBar _healthBar;

    public float MaxHealth
    {
        get { return _maxHealth; }
        protected set { _maxHealth = value; _healthBar.MaxHealth = value; }
    }
    [SerializeField] protected float _maxHealth;

    public float CurrentHealth
    {
        get { return _currentHealth; }
        protected set { _currentHealth = value; _healthBar.CurrentHealth = value; }
    }
    protected float _currentHealth;

    public void Awake()
    {
        _healthBar = GetComponent<HealthBar>();
    }

    public void Start()
    {
        _healthBar.MaxHealth = _maxHealth;
        CurrentHealth = MaxHealth;
    }

    public virtual void Die()
    {
        if (OnDeath != null) OnDeath();
    }

    public virtual void DealDamage(float damage, CombatAbility source)
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
