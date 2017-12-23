using UnityEngine;

public class BasicAttack : ICombatAbility
{
    public KeyCode HotKey { get { return KeyCode.Mouse0; } }

    public float Range { get { return 4; } }

    const float MELEE_DAMAGE = 4;

    public void PerformAbility(Transform caster)
    {
        RaycastHit hit;
        if (Physics.Raycast(caster.position, caster.forward, out hit, Range))
        {
            Character character = hit.transform.GetComponent<Character>();
            if (character != null)
            {
                character.DealDamage(MELEE_DAMAGE, caster);
            }
        }
    }
}
