using System.Collections.Generic;
using UnityEngine;

public class CombatHandler
{
    List<CombatAbility> _combatAbilities = new List<CombatAbility>
    {
        new BasicAttack()
    };

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
