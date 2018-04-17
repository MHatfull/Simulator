using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

namespace Underlunchers.Characters.AI.Navigation
{
    [RequireComponent(typeof(NavArea))]
    public class SpawnArea : MonoBehaviour
    {
        public float SpawnRadius;
        public int Quantity;
        public Character ToSpawn;

        private NavArea _navArea;
        private List<Character> _mobs = new List<Character>();

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, SpawnRadius);
        }

        public void Start()
        {
            _navArea = GetComponent<NavArea>();
            while (_mobs.Count < Quantity)
            {
                CreateMob();
            }
        }

        private void CreateMob()
        {
            var newMob = Instantiate(ToSpawn);
            newMob.GetComponent<NavMeshAgent>().Warp(_navArea.GetNextPoint());
            newMob.OnDeath += CreateMob;
            newMob.GetComponent<EnemyNavigation>().NavArea = _navArea;
            _mobs.Add(newMob);
            NetworkServer.Spawn(newMob.gameObject);
        }
    }
}