using UnityEngine.UI;
using UnityEngine;

namespace Simulator.UI.Health
{
    public class PlayerHealth : HealthDisplay
    {
        [SerializeField] Text _text;

        protected override void RenderHealth()
        {
            _text.text = _currentHealth + "/" + _maxHealth;
        }
    }
}