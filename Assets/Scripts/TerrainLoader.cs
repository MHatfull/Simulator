using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TerrainLoader : MonoBehaviour
{
    int _vertsAcross = 100;
    int _vertsDeep = 100;
    float _width = 100;
    float _depth = 100;

    List<Vector3> _verts = new List<Vector3>();
    List<Vector2> _uvs = new List<Vector2>();
    List<int> _tris = new List<int>();

    private void Awake()
    {
        byte[] terrainBytes = File.ReadAllBytes(Application.persistentDataPath + "/TerrainMesh.mesh");
        var terrainFloats = new float[terrainBytes.Length / 4];
        Buffer.BlockCopy(terrainBytes, 0, terrainFloats, 0, terrainBytes.Length);
        GenerateStartMesh(terrainFloats);
        Mesh mesh = new Mesh
        {
            vertices = _verts.ToArray(),
            triangles = _tris.ToArray(),
            uv = _uvs.ToArray()
        };
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    void GenerateStartMesh(float[] heightMap)
    {
        List<Vector3> verts = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        for (int i = 0; i < _vertsAcross; i++)
        {
            for (int j = 0; j < _vertsDeep; j++)
            {
                float h = heightMap[i * _vertsAcross + j];
                Debug.Log(h);
                verts.Add(new Vector3(((float)i / _vertsAcross) * _width, h, ((float)j / _vertsDeep) * _depth));
                uvs.Add(new Vector2((float)i / _vertsAcross, (float)j / _vertsDeep));
            }
        }
        _verts = verts;
        _tris = CalculateTriangles();
        _uvs = uvs;
    }

    private List<int> CalculateTriangles()
    {
        List<int> tris = new List<int>();
        for (int i = 0; i < _vertsAcross - 1; i++)
        {
            for (int j = 0; j < _vertsDeep - 1; j++)
            {
                tris.Add(j + (i * _vertsAcross));
                tris.Add(j + 1 + (i * _vertsAcross));
                tris.Add(j + 1 + ((i + 1) * _vertsAcross));

                tris.Add(j + (i * _vertsAcross));
                tris.Add(j + 1 + ((i + 1) * _vertsAcross));
                tris.Add(j + ((i + 1) * _vertsAcross));
            }
        }

        return tris;
    }
}
