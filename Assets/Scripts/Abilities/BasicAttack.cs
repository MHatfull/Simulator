using Simulator.Characters;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Simulator.Abilities
{
    public class BasicAttack : CombatAbility
    {
        public KeyCode HotKey { get { return KeyCode.Mouse0; } }
        public override float Range { get { return 4; } }
        public override float Cooldown { get { return 1; } }

        const float MELEE_DAMAGE = 4;

        public override bool PerformAbility(Character caster)
        {
            caster.PlayWeaponAttackAnimation();
            if (!base.PerformAbility(caster)) return false;
            base.PerformAbility(caster);
            Debug.DrawRay(caster.FoculPoint, caster.AimDirection() * Range, Color.red, 2f, false);
            IEnumerable<RaycastHit> hits;
            hits = Physics.RaycastAll(caster.FoculPoint, caster.AimDirection(), Range).ToList().Where(hit => hit.transform != caster.transform);
            if (!hits.Any()) return false;
            RaycastHit firstImpact = hits.OrderByDescending(hit => hit.distance).Last();
            Character character = firstImpact.transform.GetComponent<Character>();
            if (character != null)
            {
                character.DealDamage(MELEE_DAMAGE + caster.DamageBonus(), caster);

            }
            return true;
        }
    }
}