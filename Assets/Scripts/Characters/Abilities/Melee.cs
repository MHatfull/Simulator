using System.Linq;
using UnityEngine;

namespace Underlunchers.Characters.Abilities
{
    [CreateAssetMenu(fileName = "Melee", menuName = "Combat abilities/Melee", order = 1)]
    public class Melee : CombatAbility
    {
        public KeyCode HotKey { get { return KeyCode.Mouse0; } }

        protected override void Execute(Character caster)
        {
            Debug.DrawRay(caster.Focus.position, caster.Focus.forward * Range, Color.red, 3452f, false);
            RaycastHit[] hits;
            hits = Physics.RaycastAll(caster.Focus.position, caster.Focus.forward, Range);
            RaycastHit? firstImpact = hits.ToList().Where(hit => hit.transform != caster.transform).OrderByDescending(hit => hit.distance).LastOrDefault();
            if (firstImpact.HasValue)
            {
                if (firstImpact.Value.transform != null)
                {
                    Character character = firstImpact.Value.transform.GetComponent<Character>();
                    if (character != null)
                    {
                        character.DealDamage(Damage + caster.DamageBonus(), caster);
                    }
                }
            }
        }
    }
}