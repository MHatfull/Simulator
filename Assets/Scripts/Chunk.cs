using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Underlunchers.Scene
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class Chunk : MonoBehaviour
    {
        public delegate void VertexUpdatedHandler(Chunk chunk, Vector2Int vert, float height);
        public event VertexUpdatedHandler VertexUpdated;

        int _vertsAcross;
        int _vertsDeep;
        float _width;
        float _depth;

        Material _material;
        MeshCollider _collider;

        MeshFilter _meshFilter;
        Mesh _mesh;

        List<Vector3> _verts = new List<Vector3>();
        List<int> _tris = new List<int>();
        List<Vector2> _uvs = new List<Vector2>();

        float _dist;
        int? _selectedPoint;
        Vector3 _old;

        public static Chunk CreateChunk(Vector2 size, Vector2Int vertexSize, Material material)
        {
            GameObject newChunkObject = new GameObject();
            newChunkObject.AddComponent<MeshFilter>();
            newChunkObject.AddComponent<MeshRenderer>();
            Chunk newChunk = newChunkObject.AddComponent<Chunk>();
            newChunk._depth = size.y;
            newChunk._width = size.x;
            newChunk._vertsDeep = vertexSize.y;
            newChunk._vertsAcross = vertexSize.x;
            newChunk._material = material;
            return newChunk;
        }

        private void Start()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _collider = GetComponent<MeshCollider>();

            _mesh = GenerateStartMesh();
            _meshFilter.mesh = _mesh;
            GetComponent<MeshRenderer>().material = _material;

            _collider.sharedMesh = _mesh;
        }

        Mesh GenerateStartMesh()
        {
            List<Vector3> verts = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>();
            for (int i = 0; i < _vertsAcross; i++)
            {
                for (int j = 0; j < _vertsDeep; j++)
                {
                    Vector3 vert = new Vector3(((float)i / (_vertsAcross - 1)) * _width, 0, ((float)j / (_vertsDeep - 1)) * _depth);
                    verts.Add(vert);
                    uvs.Add(new Vector2((float)i / (_vertsAcross - 1), (float)j / (_vertsDeep - 1)));
                }
            }
            _verts = verts;
            _tris = CalculateTriangles();
            _uvs = uvs;
            Mesh mesh = new Mesh { vertices = _verts.ToArray(), triangles = _tris.ToArray(), uv = _uvs.ToArray() };
            mesh.RecalculateNormals();
            return mesh;
        }

        private List<int> CalculateTriangles()
        {
            List<int> tris = new List<int>();
            for (int i = 0; i < _vertsAcross - 1; i++)
            {
                for (int j = 0; j < _vertsDeep - 1; j++)
                {
                    tris.Add(GetIndex(i, j));
                    tris.Add(GetIndex(i, j + 1));
                    tris.Add(GetIndex(i + 1, j + 1));

                    tris.Add(GetIndex(i, j));
                    tris.Add(GetIndex(i + 1, j + 1));
                    tris.Add(GetIndex(i + 1, j));
                }
            }

            return tris;
        }

        private void OnMouseDown()
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
                _selectedPoint = _verts.IndexOf(point);
                _old = point;
                _dist = Vector3.Distance(transform.TransformPoint(point), Camera.main.transform.position);
            }
        }

        private void OnMouseUp()
        {
            if(VertexUpdated != null)
            {
                int x = _selectedPoint.Value / _vertsDeep;
                int y = _selectedPoint.Value % _vertsDeep;
                Debug.Log("changed " + x + ", " + y);
                VertexUpdated(this, new Vector2Int(x, y), _verts[_selectedPoint.Value].y);
            }
            _selectedPoint = null;
        }

        private void Update()
        {
            if (_selectedPoint.HasValue)
            {
                Vector3 raw = transform.InverseTransformPoint((Camera.main.transform.position + (Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition).direction.normalized * _dist)));
                UpdateVertex(_selectedPoint.Value, raw.y);
            }
        }

        public void UpdateVertex(Vector2Int vert, float val)
        {
            Debug.Log("updating " + vert.x + ", " + vert.y);
            UpdateVertex(GetIndex(vert.x, vert.y), val);
        }

        private void UpdateVertex(int vert, float val)
        {
            Vector2 flat = new Vector2(_verts[vert].x, _verts[vert].z);
            _verts[vert] = new Vector3(flat.x, val, flat.y);
            _mesh.vertices = _verts.ToArray();
            _mesh.RecalculateNormals();
        }

        private int GetIndex(int x, int y)
        {
            return (x * _vertsDeep) + y;
        }
    }
}
