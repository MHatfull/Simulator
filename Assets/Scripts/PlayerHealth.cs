using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : HealthDisplay
{
    Text _text;
    private void Awake()
    {
        _text = FindObjectOfType<Text>();
    }

    protected override void RenderHealth()
    {
        _text.text = _currentHealth + "/" + _maxHealth;
    }
}
