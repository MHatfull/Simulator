using System.Collections.Generic;
using System.Linq;
using Underlunchers.Characters.Abilities;
using Underlunchers.UI.Slots;
using UnityEngine;

namespace Underlunchers.Input
{
    public class HotKeyManager : MonoBehaviour
    {
        AbilitySlot[] _uiIcons;

        public static KeyCode[] HotKeys { get; private set; }
        Dictionary<KeyCode, CombatAbility> _equippedAbilities = new Dictionary<KeyCode, CombatAbility>();

        private void Awake()
        {
            HotKeys = new KeyCode[0];
        }

        public void OnConnect(PlayerAbilityController ownAbilities)
        {
            _uiIcons = FindObjectsOfType<AbilitySlot>();
            HotKeys = _uiIcons.Select(icon => icon.Key).ToArray();
            for(int i = 0; i < _uiIcons.Length; i++)
            {
                AbilitySlot slot = _uiIcons[i];
                if (ownAbilities.AvailableAbilities.Length > i)
                {
                    CombatAbility ability = ownAbilities.AvailableAbilities[i];
                    _equippedAbilities.Add(slot.Key, ability);
                    ability.AbilityCast += _uiIcons[i].ResetLoadingProgress;
                    slot.SetIcon(ability.Icon);
                    slot.SetCooldown(ability.Cooldown);
                }
            }
        }

        internal CombatAbility GetAbility(KeyCode code)
        {
            return _equippedAbilities[code];
        }
    }
}