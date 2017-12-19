using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDisplay : MonoBehaviour {
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, .5f);
    }
}
