using UnityEngine;

public class BasicAttack : CombatAbility
{
    public override KeyCode HotKey { get { return KeyCode.Mouse0; } }

    const float MELEE_RANGE = 4;
    const float MELEE_DAMAGE = 4;

    public override void PerformAbility()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit , MELEE_RANGE))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.DealDamage(MELEE_DAMAGE);
            }
        }
    }
}
