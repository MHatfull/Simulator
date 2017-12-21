public class BasicEnemy : Enemy
{
    public override void DealDamage(float damage, CombatAbility source)
    {
        base.DealDamage(damage, source);
        _enemyNavigation.Hunt(source.Caster.transform);
    }
}
