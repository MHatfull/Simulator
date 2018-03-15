using System.Linq;
using UnityEngine;

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
        RaycastHit[] hits;
        hits = Physics.RaycastAll(caster.FoculPoint, caster.AimDirection(), Range);
        RaycastHit? firstImpact = hits.ToList().Where(hit => hit.transform != caster.transform).OrderByDescending(hit => hit.distance).LastOrDefault();
        if (firstImpact.HasValue)
        {
            if (firstImpact.Value.transform != null)
            {
                Character character = firstImpact.Value.transform.GetComponent<Character>();
                if (character != null)
                {
                    character.DealDamage(MELEE_DAMAGE + caster.DamageBonus(), caster);
                }
            }
        }
        return true;
    }
}
