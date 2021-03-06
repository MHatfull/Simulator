﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

namespace Underlunchers.Characters.AI.Navigation
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Targeter))]
    public class EnemyNavigation : NetworkBehaviour
    {
        public NavArea NavArea;

        private NavMeshAgent _navMeshAgent;
        private bool _isNavigating = true;
        private Targeter _targeter;

        private void Start()
        {
            _targeter = GetComponent<Targeter>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            if (isServer)
            {
                SetDest(NavArea.GetNextPoint());
                InvokeRepeating("Navigate", 0, 0.1f);
            }
        }

        private void Navigate()
        {
            switch (_targeter.Hunting == null)
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
            var targetPos = _targeter.Hunting.transform.position;
            if (Vector3.Magnitude(targetPos - NavArea.transform.position) > NavArea.Radius)
            {
                _targeter.Hunting = null;
                SetDest(NavArea.GetNextPoint());
                return;
            }
            SetDest(targetPos, 2);
        }

        float _prevStopTime = 0;
        float _waitTime = 0;
        private void HandleWandering()
        {
            if (_navMeshAgent.remainingDistance < 1)
            {
                if (_isNavigating)
                {
                    _prevStopTime = Time.time;
                    _waitTime = Random.Range(0f, 5f);
                    _isNavigating = false;
                }
                else if (Time.time - _prevStopTime > _waitTime)
                {
                    SetDest(NavArea.GetNextPoint());
                    _isNavigating = true;
                }
            }
        }

        private void SetDest(Vector3 dest, float stoppingDistance = 0)
        {
            if (isServer)
            {
                _navMeshAgent.stoppingDistance = stoppingDistance;
                _navMeshAgent.SetDestination(dest);

                RpcSetDest(dest, stoppingDistance);
            }
        }

        [ClientRpc]
        private void RpcSetDest(Vector3 dest, float stoppingDistance)
        {
            if (_navMeshAgent)
            {
                _navMeshAgent.stoppingDistance = stoppingDistance;
                _navMeshAgent.SetDestination(dest);
            }
        }

        private IEnumerator Wait(float duration, System.Action callback)
        {
            yield return new WaitForSeconds(duration);
            callback();
        }
    }
}