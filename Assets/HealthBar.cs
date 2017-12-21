using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    TextMesh _text;

    public float CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; RenderHealth(); } }
    private float _currentHealth = 0;

    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; RenderHealth(); } }
    float _maxHealth = 0;

    private void Awake()
    {
        var healthBar = new GameObject();
        healthBar.transform.SetParent(transform);
        healthBar.transform.position = Vector3.up;
        _text = healthBar.AddComponent<TextMesh>();
        _text.alignment = TextAlignment.Center;
        _text.anchor = TextAnchor.MiddleCenter;
        RenderHealth();
    }

    private void Update()
    {
        if (Camera.main != null)
        {
            transform.LookAt(2* transform.position - Camera.main.transform.position);
        }
    }

    private void RenderHealth()
    {
        _text.text = _currentHealth + "/" + _maxHealth;
    }
}
