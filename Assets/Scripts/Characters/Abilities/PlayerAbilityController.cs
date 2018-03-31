using System.Linq;
using Underlunchers.Characters.Player;
using Underlunchers.Input;
using Underlunchers.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace Underlunchers.Characters.Abilities
{
    [RequireComponent(typeof(PlayerCharacter))]
    public class PlayerAbilityController : AbilityController
    {
        private PlayerCharacter _owner;
        HotKeyManager _hotKeys;

        private void Awake()
        {
            _owner = GetComponent<PlayerCharacter>();
            _hotKeys = Object.FindObjectOfType<HotKeyManager>();
            _hotKeys.OnConnect(this);
        }

        private void Start()
        {
            if (isLocalPlayer)
            {
                InputManager.HotKeyDown += HotKeyDown;
            }
        }

        private void HotKeyDown(KeyCode code)
        {
            var ability = _hotKeys.GetAbility(code);
            CmdHandleCombat(ability.name, Camera.main.transform.position, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1)) - Camera.main.transform.position);
        }

        [Command]
        public void CmdHandleCombat(string abilityName, Vector3 focalPoint, Vector3 focalDirection)
        {
            _owner.UpdateFocus(focalPoint, focalDirection);
            CombatAbility casting = null;
            foreach(var a in AvailableAbilities)
            {
                if(a.name == abilityName)
                {
                    casting = a;
                }
            }
            casting.PerformAbility(_owner);
        }
    }
}