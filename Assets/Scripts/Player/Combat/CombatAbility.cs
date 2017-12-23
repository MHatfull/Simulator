using UnityEngine;

public interface ICombatAbility {
    void PerformAbility(Transform caster);
    float Range { get; }
}
