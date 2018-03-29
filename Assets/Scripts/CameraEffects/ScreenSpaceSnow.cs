using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScreenSpaceSnow : MonoBehaviour {

    public Texture2D SnowTexture;
    public Color SnowColor = Color.white;
    public float SnowTextureScale = .1f;
    [Range(0, 1)] public float BottomThreshold = 0f;
    [Range(0, 1)] public float TopThreshold = 1f;

    private Material _material;

    private Camera _camera;

    private void OnEnable()
    {
        _material = new Material(Shader.Find("CameraEffects/ScreenSpaceSnow"));
        _camera = GetComponent<Camera>();
        _camera.depthTextureMode = DepthTextureMode.DepthNormals;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(_camera == null) _camera = GetComponent<Camera>();
        SetMaterialProperties();
        DrawSnow(source, destination);
    }

    private void DrawSnow(RenderTexture source, RenderTexture dest)
    {
        Graphics.Blit(source, dest, _material);
    }

    private void SetMaterialProperties()
    {
        Debug.Log("setting cam 2 world matrix");
        _material.SetMatrix("_CamToWorld", _camera.cameraToWorldMatrix);
        Debug.Log("setting snow color");
        _material.SetColor("_SnowColor", SnowColor);
        Debug.Log("setting bottom thresh");
        _material.SetFloat("_BottomThreshold", BottomThreshold);
        Debug.Log("setting top thresh");
        _material.SetFloat("_TopThreshold", TopThreshold);
        Debug.Log("setting snow tex");
        _material.SetTexture("_SnowTex", SnowTexture);
        Debug.Log("setting snow tex scale");
        _material.SetFloat("_SnowTexScale", SnowTextureScale);
    }


}
