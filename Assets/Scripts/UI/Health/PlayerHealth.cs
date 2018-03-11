using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : HealthDisplay
{
    Text _text;

    public void SetupText(Text text)
    {
        _text = text;
    }

    protected override void RenderHealth()
    {
        if (isLocalPlayer)
        {
            if (!_text)
            {
                SetupText(GameObject.Find("HealthDisplay").GetComponent<Text>());
            }
        }
        _text.text = _currentHealth + "/" + _maxHealth;
    }
}
