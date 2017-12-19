using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavigation : MonoBehaviour {

    public NavArea NavArea;

    private NavMeshAgent _navMeshAgent;

    
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(NavArea.GetNextPoint().Value);
    }

    private void Update()
    {
        if(_navMeshAgent.remainingDistance < 1)
        {
            _navMeshAgent.SetDestination(NavArea.GetNextPoint().Value);
        }
    }

}
