using UnityEngine;

public class BasicEnemy : Enemy
{
    public override void DealDamage(float damage, Transform source)
    {
        base.DealDamage(damage, source);
        _enemyNavigation.Hunt(source);
    }
}
