using System.Linq;
using Underlunchers.Characters.AI;
using Underlunchers.Characters.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Underlunchers.Characters.Abilities
{
    [RequireComponent(typeof(EnemyNavigation))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Enemy))]
    public class EnemyAbilityController : AbilityController
    {

        NavMeshAgent _navMeshAgent;
        //EnemyNavigation _enemyNavigation;
        Enemy _self;

        protected void Awake()
        {
            _self = GetComponent<Enemy>();
            //_enemyNavigation = GetComponent<EnemyNavigation>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (_self.Hunting && _navMeshAgent.remainingDistance < 4)
            {
                var cooled = AvailableAbilities.Where(v => !v.isOnCooldown()).ToList();
                if (cooled.Any()) cooled[0].PerformAbility(_self);
            }
        }
    }
}
