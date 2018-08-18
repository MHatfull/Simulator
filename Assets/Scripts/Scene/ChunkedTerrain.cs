using System;
using System.Collections;
using System.Collections.Generic;
using Underlunchers.Networking;
using UnityEngine;

namespace Underlunchers.Scene
{
    public class ChunkedTerrain : MonoBehaviour
    {
        [SerializeField] Vector2Int _chunkVerts;
        [SerializeField] Vector2 _chunkSize;
        [SerializeField] Vector2Int _numChunks;
        [SerializeField] private Material _material;
        [SerializeField] private bool _loadMesh;

        Chunk[][] _chunks;
        [SerializeField] Dictionary<Vector2Int, List<Chunk>> _chunksAtVerts = new Dictionary<Vector2Int, List<Chunk>>();
        private readonly Dictionary<Chunk, Vector2Int> _chunkLocations = new Dictionary<Chunk, Vector2Int>();
        private float[][] _heightMap;

        private void Awake()
        {
            _chunks = new Chunk[_numChunks.x][];
            if (_loadMesh)
            {
                string metadata = System.IO.File.ReadAllText(Application.persistentDataPath + "/TerrainMetaData.terrain");
                string[] split = metadata.Split(',');
                _chunkVerts = new Vector2Int(int.Parse(split[0]), int.Parse(split[1]));
                _numChunks = new Vector2Int(int.Parse(split[2]), int.Parse(split[3]));
                _chunkSize = new Vector2(float.Parse(split[4]), float.Parse(split[5]));
                _chunks = new Chunk[_numChunks.x][];
                LoadMesh();
            }
            else
            {
                _heightMap = new float[_numChunks.x * (_chunkVerts.x - 1) + 1][];
            }
            CreateStartChunks();
        }

        private void LoadMesh()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/TerrainData.terrain");
            var flat = new float[bytes.Length / 4];
            Buffer.BlockCopy(bytes, 0, flat, 0, bytes.Length);
            _heightMap = new float[_numChunks.x * (_chunkVerts.x - 1) + 1][];
            for (int i = 0; i < _heightMap.Length; i++)
            {
                _heightMap[i] = new float[_numChunks.y * (_chunkVerts.y - 1) + 1];
                for (int j = 0; j < _heightMap[0].Length; j++)
                {
                    _heightMap[i][j] = flat[i * _heightMap.Length + j];
                }
            }
        }

        private void Update()
        {
            for (int i = 0; i < _chunks.Length; i++)
            {
                if (_chunks[i] != null)
                {
                    for (int j = 0; j < _chunks[i].Length; j++)
                    {
                        if (_chunks[i][j])
                            _chunks[i][j].UpdateChunk();
                    }
                }
            }
        }

        public void SaveMesh()
        {
            float[] flatArray = new float[_heightMap.Length * _heightMap[0].Length];
            for (int i = 0; i < _heightMap.Length; i++)
            {
                for (int j = 0; j < _heightMap[0].Length; j++)
                {
                    flatArray[i * _heightMap.Length + j] = _heightMap[i][j];
                }
            }
            var byteArray = new byte[flatArray.Length * 4];
            Buffer.BlockCopy(flatArray, 0, byteArray, 0, byteArray.Length);
            string metadata = _chunkVerts.x + "," + _chunkVerts.y + "," + _numChunks.x + "," + _numChunks.y + "," + _chunkSize.x + "," + _chunkSize.y;
            TerrainUploader uploader = FindObjectOfType<TerrainUploader>();
            uploader.SaveByes(byteArray, "terrain.terrain", name);
            uploader.SaveString(metadata, "metaData.json", name);
            uploader.Upload(name);
        }

        private void CreateStartChunks()
        {
            StartCoroutine(CreateStartChunksRoutine());
        }

        private IEnumerator CreateStartChunksRoutine()
        {
            for (int i = 0; i < _numChunks.x; i++)
            {
                _chunks[i] = new Chunk[_numChunks.y];
                for (int j = 0; j < _numChunks.y; j++)
                {
                    Chunk chunk = CreateChunk(i, j);
                    Vector2Int chunkLoc = new Vector2Int(i * (_chunkVerts.x - 1), j * (_chunkVerts.y - 1));
                    _chunkLocations.Add(chunk, chunkLoc);
                    PrefillHeightmap(i, j, chunk);
                }
            }
            yield return new WaitForEndOfFrame();
            for(int i = 0; i < _heightMap.Length; i++)
            {
                for(int j = 0; j < _heightMap[0].Length; j++)
                {
                    UpdateChunks(new Vector2Int(i, j));
                }
            }
            yield return new WaitForEndOfFrame();
            for (int i = 0; i < _chunks.Length; i++)
            {
                for (int j = 0; j < _chunks.Length; j++)
                {
                    _chunks[i][j].BuildNavmesh();
                }
            }
        }

        private void PrefillHeightmap(int i, int j, Chunk c)
        { 
            for (int x = 0; x < _chunkVerts.x; x++)
            {
                if (!_loadMesh)
                {
                    _heightMap[i * (_chunkVerts.x - 1) + x] = new float[_numChunks.y * (_chunkVerts.y - 1) + 1];
                }
                for (int y = 0; y < _chunkVerts.y; y++)
                {
                    Vector2Int v = new Vector2Int(i * (_chunkVerts.x - 1) + x, j * (_chunkVerts.y - 1) + y);
                    if (!_chunksAtVerts.ContainsKey(v))
                    {
                        _chunksAtVerts.Add(v, new List<Chunk> { c });
                    }
                    else
                    {
                        _chunksAtVerts[v].Add(c);
                    }
                    if (!_loadMesh)
                    {
                        _heightMap[v.x][v.y] = 0;
                    }
                }
            }
        }

        private Chunk CreateChunk(int i, int j)
        {
            Chunk chunk = Chunk.CreateChunk(_chunkSize, _chunkVerts, _material);
            chunk.transform.parent = transform;
            chunk.transform.position = new Vector3(i * _chunkSize.x, 0, j * _chunkSize.y);
            chunk.VertexUpdated += Smooth;
            _chunks[i][j] = chunk;
            return chunk;
        }

        private void Smooth(Chunk chunk, Vector2Int vert, float height)
        {
            Vector2Int l = _chunkLocations[chunk];
            _heightMap[l.x + vert.x][l.y + vert.y] = height;
            List<Vector2Int> toUpdate = new List<Vector2Int> { new Vector2Int(l.x + vert.x, l.y + vert.y) };

            UpdateChunks(toUpdate);
        }

        private void UpdateChunks(List<Vector2Int> updated)
        {
            foreach(Vector2Int pos in updated)
            {
                UpdateChunks(pos);
            }
        }

        private void UpdateChunks(Vector2Int pos)
        {
            float height = _heightMap[pos.x][pos.y];
            foreach (Chunk c in _chunksAtVerts[pos])
            {
                Vector2Int chunkLocation = _chunkLocations[c];
                c.UpdateVertex(pos - chunkLocation, height);
            }
            foreach (Chunk c in _chunksAtVerts[pos])
            {
                for(int i = 0; i < _chunkVerts.x; i++)
                {
                    for(int j = 0; j< _chunkVerts.y; j++)
                    {
                        Vector2Int chunkLocation = _chunkLocations[c];
                        RecalculateNormals(new Vector2Int(i + chunkLocation.x, j + chunkLocation.y));
                    }
                }
            }
        }

        private void RecalculateNormals(Vector2Int pos)
        {
            Vector3 normal = Vector3.zero;
            foreach (Chunk c in _chunksAtVerts[pos])
            {
                Vector2Int chunkLocation = _chunkLocations[c];
                normal += c.NormalAt(pos - chunkLocation);
            }
            foreach (Chunk c in _chunksAtVerts[pos])
            {
                Vector2Int chunkLocation = _chunkLocations[c];
                c.UpdateNormal(pos - chunkLocation, normal.normalized);
            }
        }
    }
}