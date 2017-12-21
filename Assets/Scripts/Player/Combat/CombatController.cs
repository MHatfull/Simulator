using System.Collections.Generic;
using UnityEngine;

public class CombatHandler
{
    List<CombatAbility> _combatAbilities = new List<CombatAbility>();

    private Transform _owner;

    public CombatHandler(Transform owner)
    {
        _owner = owner;
        _combatAbilities.Add(new BasicAttack(_owner));
    }

    public void HandleCombat()
    {
        if (Input.anyKeyDown)
        {
            foreach (CombatAbility ability in _combatAbilities)
            {
                if (Input.GetKeyDown(ability.HotKey))
                {
                    ability.PerformAbility();
                }
            }
        }
    }
}
