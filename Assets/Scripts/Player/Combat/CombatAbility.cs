using UnityEngine;

public abstract class CombatAbility {
    public virtual bool PerformAbility(Character caster)
    {
        if (isOnCooldown()) return false;
        _lastFireTime = Time.time;
        return true;
    }

    public abstract float Range { get; }
    protected abstract float Cooldown { get; }

    private float _lastFireTime = 0;
    public bool isOnCooldown()
    {
        return Time.time - _lastFireTime < Cooldown;
    }
}
