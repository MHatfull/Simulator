using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyNavigation))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAbilityController : AbilityController {

    NavMeshAgent _navMeshAgent;
    EnemyNavigation _enemyNavigation;

    protected override void Awake()
    {
        base.Awake();
        _enemyNavigation = GetComponent<EnemyNavigation>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_enemyNavigation.HuntingTarget && _navMeshAgent.remainingDistance < 4)
        {
            var cooled = _availableAbilities.Values.Where(v => !v.isOnCooldown()).ToList();
            if(cooled.Any()) cooled[0].PerformAbility(transform);
        }
    }
}
