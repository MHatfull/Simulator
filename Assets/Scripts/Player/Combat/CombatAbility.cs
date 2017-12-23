using UnityEngine;

public interface ICombatAbility {
    void PerformAbility(Transform caster);
    KeyCode HotKey { get; }
    float Range { get; }
}
