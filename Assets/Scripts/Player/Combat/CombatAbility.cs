using UnityEngine;

public abstract class CombatAbility {
    public abstract void PerformAbility();
    public abstract KeyCode HotKey { get; }
}
