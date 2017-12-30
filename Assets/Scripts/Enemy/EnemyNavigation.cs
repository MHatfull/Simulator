using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Enemy))]
public class EnemyNavigation : MonoBehaviour {

    public NavArea NavArea;

    private NavMeshAgent _navMeshAgent;
    private bool _isNavigating = true;
    private Enemy _self;

    private void Start()
    {
        _self = GetComponent<Enemy>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(NavArea.GetNextPoint());
        InvokeRepeating("Navigate", 0, 0.1f);
    }

    private void Navigate()
    {
        switch(_self.Hunting == null)
        {
            case true:
                HandleWandering();
                break;
            case false:
                HandleFollow();
                break;
        }
    }

    private void HandleFollow()
    {
        var targetPos = _self.Hunting.transform.position;
        if(Vector3.Magnitude(targetPos - NavArea.transform.position) > NavArea.Radius)
        {
            _self.Hunting = null;
            _navMeshAgent.SetDestination(NavArea.GetNextPoint());
            return;
        }
        _navMeshAgent.stoppingDistance = 2;
        _navMeshAgent.SetDestination(targetPos);
    }

    private void HandleWandering()
    {
        if (_navMeshAgent.remainingDistance < 1 && _isNavigating)
        {
            _isNavigating = false;
            StartCoroutine(Wait(Random.Range(0f, 5f),
                () =>
                {
                    _navMeshAgent.stoppingDistance = 0;
                    _navMeshAgent.SetDestination(NavArea.GetNextPoint());
                    _isNavigating = true;
                }));
        }
    }

    private IEnumerator Wait(float duration, System.Action callback)
    {
        yield return new WaitForSeconds(duration);
        callback();
    }
}
