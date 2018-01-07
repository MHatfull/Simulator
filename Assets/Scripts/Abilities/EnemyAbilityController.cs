using Simulator.Characters;
using Simulator.Characters.AI;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Simulator.Abilities
{
    [RequireComponent(typeof(EnemyNavigation))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Enemy))]
    public class EnemyAbilityController : AbilityController
    {

        NavMeshAgent _navMeshAgent;
        EnemyNavigation _enemyNavigation;
        Enemy _self;

        protected override void Awake()
        {
            base.Awake();
            _self = GetComponent<Enemy>();
            _enemyNavigation = GetComponent<EnemyNavigation>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (_self.Hunting && _navMeshAgent.remainingDistance < 4)
            {
                var cooled = AvailableAbilities.Values.Where(v => !v.isOnCooldown()).ToList();
                if (cooled.Any()) cooled[0].PerformAbility(_self);
            }
        }
    }
}