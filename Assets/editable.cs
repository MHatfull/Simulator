using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Underlunchers.MapCreator
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class Editable : MonoBehaviour
    {

        MeshFilter _meshFilter;
        Mesh _mesh;
        MeshCollider _meshCollider;
        List<Vector3> _verts = new List<Vector3> { new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(1, 0, 1) };
        List<int> _tris = new List<int> { 0, 1, 2, 3, 2, 1 };
        List<Vector2> _uvs = new List<Vector2> { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) };
        int? _nearestPoint;
        Vector3 _old;
        float _dist;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshCollider = GetComponent<MeshCollider>();
            _verts = _meshFilter.mesh.vertices.ToList();
            _tris = _meshFilter.mesh.triangles.ToList();
            _uvs = _meshFilter.mesh.uv.ToList();
            _mesh = new Mesh
            {
                vertices = _verts.ToArray(),
                triangles = _tris.ToArray(),
                uv = _uvs.ToArray()
            };
            _mesh.RecalculateNormals();
            _meshFilter.mesh = _mesh;
            _meshCollider.sharedMesh = _mesh;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                GetNearestPoint();
            }
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                _nearestPoint = null;
                _meshCollider.sharedMesh = _mesh;
                SmoothMesh();
            }
            if (_nearestPoint.HasValue)
            {
                Vector3 raw = transform.InverseTransformPoint((Camera.main.transform.position + (Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition).direction.normalized * _dist)));
                _verts[_nearestPoint.Value] = new Vector3(_old.x, raw.y, _old.z);
                _mesh.vertices = _verts.ToArray();
                _mesh.RecalculateNormals();
            }
            _dist += UnityEngine.Input.GetAxis("Mouse ScrollWheel");
        }

        private void SmoothMesh()
        {
            List<Vector3> newVerts = new List<Vector3>();
            foreach(Vector3 vert in _verts)
            {
                if (Vector3.Distance(new Vector3(_old.x, 0, _old.z), new Vector3(vert.x, 0, vert.z)) < 5)
                {
                    newVerts.Add(new Vector3(vert.x, Nearest(vert, 1.5f).Average(p => p.y), vert.z));
                }
                else
                {
                    newVerts.Add(vert);
                }
            }
            _verts = newVerts;
            _mesh.vertices = _verts.ToArray();
            _mesh.RecalculateNormals();
            _meshCollider.sharedMesh = _mesh;
        }

        List<Vector3> Nearest(Vector3 origin, float radius)
        {
            return _verts.Where(v => Vector3.Distance(new Vector3(origin.x,0,origin.z), new Vector3(v.x, 0, v.z)) < radius).ToList();
        }

        private void GetNearestPoint()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                List<Vector3> triangle = new List<Vector3>
                    {
                        _mesh.vertices[_mesh.triangles[hit.triangleIndex * 3]],
                        _mesh.vertices[_mesh.triangles[hit.triangleIndex * 3 + 1]],
                        _mesh.vertices[_mesh.triangles[hit.triangleIndex * 3 + 2]]
                    };
                Vector3 point = triangle.OrderBy((a) => Vector3.Distance(a, hit.transform.InverseTransformPoint(hit.point))).ToList()[0];
                _nearestPoint = _verts.IndexOf(point);
                _old = point;
                _dist = Vector3.Distance(transform.TransformPoint(point), Camera.main.transform.position);
            }
        }
    }
}