using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavArea))]

public class SpawnArea : MonoBehaviour
{
    public float SpawnRadius;
    public int Quantity;
    public Enemy ToSpawn;

    private NavArea _navArea;
    private List<Enemy> _mobs = new List<Enemy>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SpawnRadius);
    }

    public void Start()
    {
        _navArea = GetComponent<NavArea>();
        while(_mobs.Count < Quantity)
        {
            CreateMob();
        }
    }

    private void CreateMob()
    {
        var newMob = Instantiate(ToSpawn);
        newMob.GetComponent<NavMeshAgent>().Warp(_navArea.GetNextPoint());
        newMob.OnDeath += CreateMob;
        newMob.NavArea = _navArea;
        _mobs.Add(newMob);
    }
}
