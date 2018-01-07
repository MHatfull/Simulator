using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simulator.UI
{
    public class GizmoDisplay : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position, .5f);
        }
    }
}