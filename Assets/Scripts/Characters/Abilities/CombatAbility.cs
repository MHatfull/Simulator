using UnityEngine;

namespace Underlunchers.Characters.Abilities
{
    public abstract class CombatAbility : ScriptableObject
    {
        public delegate void AbilityCastHanlder();
        public event AbilityCastHanlder AbilityCast;
        public Sprite Icon;
        public string UniqueName;
        public float Range;
        public float Cooldown;
        public float Damage;

        public virtual bool PerformAbility(Character caster)
        {
            Debug.Log(caster + " performing ability");
            if (isOnCooldown()) return false;
            if (AbilityCast != null)
            {
                AbilityCast();
            }
            _lastFireTime = Time.time;
            return true;
        }

        private float _lastFireTime = -Mathf.Infinity;
        public bool isOnCooldown()
        {
            return Time.time - _lastFireTime < Cooldown;
        }
    }
}