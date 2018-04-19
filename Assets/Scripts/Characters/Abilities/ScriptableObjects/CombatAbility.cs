using System.Collections.Generic;
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

        public void PerformAbility(Character caster)
        {
            Debug.Log(caster + " performing ability");
            if (IsOnCooldown(caster)) return;
            else Debug.Log("not on cooldown continuing");
            Execute(caster);
            if (AbilityCast != null)
            {
                AbilityCast();
            }
            if (_lastFireTimes.ContainsKey(caster))
            {
                _lastFireTimes[caster] = Time.time;
            }
            else
            {
                _lastFireTimes.Add(caster, Time.time);
            }
        }

        protected abstract void Execute(Character character);

        private Dictionary<Character, float> _lastFireTimes = new Dictionary<Character, float>();
        public bool IsOnCooldown(Character who)
        {
            if (_lastFireTimes.ContainsKey(who))
            {
                return Time.time - _lastFireTimes[who] < Cooldown;
            }
            else
            {
                return false;
            }
        }
    }
}