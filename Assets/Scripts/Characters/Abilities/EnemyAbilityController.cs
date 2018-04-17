using System.Linq;
using Underlunchers.Characters.AI;
using Underlunchers.Characters.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Underlunchers.Characters.Abilities
{
    [RequireComponent(typeof(EnemyNavigation))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Targeter))]
    public class EnemyAbilityController : AbilityController
    {
        NavMeshAgent _navMeshAgent;
        Targeter _targeter;

        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _targeter = GetComponent<Targeter>();
        }

        private void Update()
        {
            if (_targeter.Hunting && _navMeshAgent.remainingDistance < 4)
            {
                var cooled = AvailableAbilities.Where(v => !v.IsOnCooldown(Owner)).ToList();
                if (cooled.Any()) cooled[0].PerformAbility(Owner);
            }
        }
    }
}
