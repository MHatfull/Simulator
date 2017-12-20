using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavArea))]

public class SpawnArea : MonoBehaviour
{
    public float SpawnRadius;
    public int Quantity;
    public Spawnable ToSpawn;

    private NavArea _navArea;
    private List<Spawnable> _mobs = new List<Spawnable>();

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
        newMob.OnDeath += CreateMob;
        newMob.NavArea = _navArea;
        _mobs.Add(newMob);
    }
}
