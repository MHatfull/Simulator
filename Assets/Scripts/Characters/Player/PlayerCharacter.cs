using Simulator.Abilities;
using Simulator.Items;
using UnityEngine;

namespace Simulator.Characters.Player
{
    [RequireComponent(typeof(PlayerAbilityController))]
    public class PlayerCharacter : Character
    {
        public static PlayerCharacter Me;
        [SerializeField] Transform _rightHand;
        public static Transform RightHand;

        protected override void Awake()
        {
            base.Awake();
            Me = this;
            RightHand = _rightHand;
        }

        [SerializeField] Transform _foculPoint;
        public override Vector3 FoculPoint
        {
            get
            {
                return _foculPoint.transform.position;
            }
        }

        public override Vector3 AimDirection()
        {
            return Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1)) - Camera.main.transform.position;
        }

        public override void DealDamage(float damage, Character source)
        {
            base.DealDamage(Mathf.Clamp(damage - EquipmentManager.DamageReduction(), 0, Mathf.Infinity), source);
        }

        internal override float DamageBonus()
        {
            return EquipmentManager.Damage();
        }

        public override void PlayWeaponAttackAnimation()
        {
            _animator.SetTrigger("SwingSword");
        }
    }
}