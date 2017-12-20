using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Enemy))]
public class EnemyNavigation : MonoBehaviour {

    public NavArea NavArea;

    private NavMeshAgent _navMeshAgent;

    
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(NavArea.GetNextPoint());
    }

    private void Update()
    {
        if(_navMeshAgent.remainingDistance < 1)
        {
            _navMeshAgent.SetDestination(NavArea.GetNextPoint());
        }
    }

}
