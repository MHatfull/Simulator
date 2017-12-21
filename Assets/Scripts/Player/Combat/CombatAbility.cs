using UnityEngine;

public abstract class CombatAbility {
    public abstract void PerformAbility();
    public abstract KeyCode HotKey { get; }
    public Transform Caster { get; private set; }
    public CombatAbility(Transform caster)
    {
        Caster = caster;
    }
}
