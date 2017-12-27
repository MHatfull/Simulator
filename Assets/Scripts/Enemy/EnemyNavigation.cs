using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Enemy))]
public class EnemyNavigation : MonoBehaviour {

    public NavArea NavArea;

    private enum NavMode { Wander, Hunting }
    private NavMode _navMode = NavMode.Wander;
    private NavMeshAgent _navMeshAgent;
    private bool _isNavigating = true;
    public Character HuntingTarget;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(NavArea.GetNextPoint());
        InvokeRepeating("Navigate", 0, 0.1f);
    }

    public void Hunt(Character target)
    {
        _navMode = NavMode.Hunting;
        HuntingTarget = target;
    }

    private void Navigate()
    {
        switch(_navMode)
        {
            case NavMode.Wander:
                HandleWandering();
                break;
            case NavMode.Hunting:
                HandleFollow();
                break;
        }
    }

    private void HandleFollow()
    {
        var targetPos = HuntingTarget.transform.position;
        if(Vector3.Magnitude(targetPos - NavArea.transform.position) > NavArea.Radius)
        {
            _navMode = NavMode.Wander;
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
