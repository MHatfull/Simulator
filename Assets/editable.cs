using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class editable : MonoBehaviour {

    MeshFilter _meshFilter;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
    }
}
