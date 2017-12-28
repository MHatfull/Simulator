using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyNavigation))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Character))]
public class EnemyAbilityController : AbilityController {

    NavMeshAgent _navMeshAgent;
    EnemyNavigation _enemyNavigation;
    Character _self;

    protected override void Awake()
    {
        base.Awake();
        _self = GetComponent<Character>();
        _enemyNavigation = GetComponent<EnemyNavigation>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_enemyNavigation.HuntingTarget && _navMeshAgent.remainingDistance < 4)
        {
            var cooled = AvailableAbilities.Values.Where(v => !v.isOnCooldown()).ToList();
            if(cooled.Any()) cooled[0].PerformAbility(_self);
        }
    }
}
