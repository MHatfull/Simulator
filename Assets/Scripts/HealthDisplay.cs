using UnityEngine;

public abstract class HealthDisplay : MonoBehaviour {

    public float CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; RenderHealth(); } }
    protected float _currentHealth = 0;

    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; RenderHealth(); } }
    protected float _maxHealth = 0;

    protected abstract void RenderHealth();

}
