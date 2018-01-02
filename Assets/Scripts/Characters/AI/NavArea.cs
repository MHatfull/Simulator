using UnityEngine;
using UnityEngine.AI;

public class NavArea : MonoBehaviour {

    public float Radius;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    public Vector3 GetNextPoint()
    {
        Vector2 pos = Random.insideUnitCircle * Random.Range(0, Radius);
        Vector3 sample = new Vector3(transform.position.x + pos.x, 0, transform.position.z + pos.y);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(sample, out hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else return transform.position;
    }
}
