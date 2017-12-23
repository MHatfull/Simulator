using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityController : MonoBehaviour {
    protected enum Ability { BasicAttack }
    private Dictionary<Ability, System.Func<ICombatAbility>> _abilityFactories = new Dictionary<Ability, System.Func<ICombatAbility>>
    {
        { Ability.BasicAttack, () => { return new BasicAttack(); } }
    };
    protected List<ICombatAbility> _availableAbilities = new List<ICombatAbility>();
    [SerializeField] protected Ability[] _abilities;

    private void Awake()
    {
        foreach(var ability in _abilities)
        {
            _availableAbilities.Add(_abilityFactories[ability]());
        }
    }
}
