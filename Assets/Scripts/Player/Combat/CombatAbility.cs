using UnityEngine;

public abstract class CombatAbility {

    public delegate void AbilityCastHanlder();
    public event AbilityCastHanlder AbilityCast;

    public virtual bool PerformAbility(Character caster)
    {
        if (isOnCooldown()) return false;
        if (AbilityCast != null) AbilityCast();
        _lastFireTime = Time.time;
        return true;
    }

    public abstract float Range { get; }
    public abstract float Cooldown { get; }

    private float _lastFireTime = -Mathf.Infinity;
    public bool isOnCooldown()
    {
        return Time.time - _lastFireTime < Cooldown;
    }
}
