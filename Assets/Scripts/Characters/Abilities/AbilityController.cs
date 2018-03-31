using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Underlunchers.Characters.Abilities
{
    public abstract class AbilityController : NetworkBehaviour
    {
        public CombatAbility[] AvailableAbilities;
    }
}
