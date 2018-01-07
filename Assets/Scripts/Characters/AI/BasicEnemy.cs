using UnityEngine;

namespace Simulator.Characters.AI
{
    public class BasicEnemy : Enemy
    {
        public override Vector3 FoculPoint
        {
            get
            {
                return transform.position;
            }
        }

        public override void DealDamage(float damage, Character source)
        {
            base.DealDamage(damage, source);
            Hunting = source;
        }

        public override Vector3 AimDirection()
        {
            return (Hunting.transform.position - transform.position).normalized;
        }

        internal override float DamageBonus()
        {
            return 0;
        }

        public override void PlayWeaponAttackAnimation()
        {

        }
    }
}