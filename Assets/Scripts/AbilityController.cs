using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityController : MonoBehaviour {
    public enum Ability { BasicAttack }
    private Dictionary<Ability, System.Func<CombatAbility>> _abilityFactories = new Dictionary<Ability, System.Func<CombatAbility>>
    {
        { Ability.BasicAttack, () => { return new BasicAttack(); } }
    };
    protected Dictionary<Ability, CombatAbility> _availableAbilities = new Dictionary<Ability, CombatAbility>();
    [SerializeField] protected Ability[] _abilities;

    protected void Awake()
    {
        foreach(var ability in _abilities)
        {
            Debug.Log("adding ability " + ability + " " + name);
            _availableAbilities.Add(ability, _abilityFactories[ability]());
        }
    }
}
