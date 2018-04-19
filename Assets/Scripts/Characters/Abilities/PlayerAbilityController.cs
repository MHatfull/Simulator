using Underlunchers.Input;
using UnityEngine;
using UnityEngine.Networking;

namespace Underlunchers.Characters.Abilities
{
    public class PlayerAbilityController : AbilityController
    {
        HotKeyManager _hotKeys;

        private void Start()
        {
            if (isLocalPlayer)
            {
                _hotKeys = Object.FindObjectOfType<HotKeyManager>();
                _hotKeys.OnConnect(this);
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
            CombatAbility casting = null;
            foreach(var a in AvailableAbilities)
            {
                if(a.name == abilityName)
                {
                    casting = a;
                }
            }
            casting.PerformAbility(Owner);
        }
    }
}