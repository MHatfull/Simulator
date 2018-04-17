using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Underlunchers.Characters.Abilities
{
    [RequireComponent(typeof(Character))]
    public abstract class AbilityController : NetworkBehaviour
    {
        public CombatAbility[] AvailableAbilities;
        protected Character Owner { get; private set; }

        protected virtual void Awake()
        {
            Owner = GetComponent<Character>();
        }
    }
}
