using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class AbilityController : NetworkBehaviour {
    public enum Ability { BasicAttack }
    private Dictionary<Ability, System.Func<CombatAbility>> _abilityFactories = new Dictionary<Ability, System.Func<CombatAbility>>
    {
        { Ability.BasicAttack, () => { return new BasicAttack(); } }
    };
    public Dictionary<Ability, CombatAbility> AvailableAbilities = new Dictionary<Ability, CombatAbility>();
    [SerializeField] protected Ability[] _abilities;

    protected virtual void Awake()
    {
        AvailableAbilities.Clear();
        foreach(var ability in _abilities)
        {
            AvailableAbilities.Add(ability, _abilityFactories[ability]());
        }
    }
}
