using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simulator.UI
{
    [ExecuteInEditMode]
    public class ScreenSpaceSnow : MonoBehaviour
    {

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
            if (_camera == null) _camera = GetComponent<Camera>();
            _material.SetMatrix("_CamToWorld", _camera.cameraToWorldMatrix);
            _material.SetColor("_SnowColor", SnowColor);
            _material.SetFloat("_BottomThreshold", BottomThreshold);
            _material.SetFloat("_TopThreshold", TopThreshold);
            _material.SetTexture("_SnowTex", SnowTexture);
            _material.SetFloat("_SnowTexScale", SnowTextureScale);
            Graphics.Blit(source, destination, _material);
        }
    }
}