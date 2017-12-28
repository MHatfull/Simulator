using UnityEngine;

public class BasicAttack : CombatAbility
{
    public KeyCode HotKey { get { return KeyCode.Mouse0; } }

    public override float Range { get { return 4; } }

    protected override float Cooldown { get { return 4; } }

    const float MELEE_DAMAGE = 4;

    public override bool PerformAbility(Character caster)
    {
        if (!base.PerformAbility(caster)) return false;
        base.PerformAbility(caster);
        Debug.DrawRay(caster.FoculPoint, caster.AimDirection(), Color.red, 2f, false);
        RaycastHit hit;
        if (Physics.Raycast(caster.FoculPoint, caster.AimDirection(), out hit, Range))
        {
            Character character = hit.transform.GetComponent<Character>();
            if (character != null)
            {
                character.DealDamage(MELEE_DAMAGE, caster);
            }
        }
        return true;
    }
}
