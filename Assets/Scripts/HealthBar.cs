using UnityEngine;

public class HealthBar : HealthDisplay {
    TextMesh _text;

    GameObject _healthBar;

    private void Awake()
    {
        _healthBar = new GameObject();
        _healthBar.transform.SetParent(transform);
        _healthBar.transform.position = Vector3.up;
        _text = _healthBar.AddComponent<TextMesh>();
        _text.alignment = TextAlignment.Center;
        _text.anchor = TextAnchor.MiddleCenter;
        RenderHealth();
    }

    private void Update()
    {
        if (Camera.main != null)
        {
            _healthBar.transform.LookAt(2* transform.position - Camera.main.transform.position);
        }
    }

    protected override void RenderHealth()
    {
        _text.text = _currentHealth + "/" + _maxHealth;
    }
}
