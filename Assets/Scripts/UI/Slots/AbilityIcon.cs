using UnityEngine;
using UnityEngine.UI;

namespace Simulator.UI.Slots
{
    public class AbilityIcon : UISlot
    {

        public KeyCode Key;

        float _cooldown = 1;


        public void ResetLoadingProgress()
        {
            _image.fillAmount = 0;
            var color = _image.color;
            color.a = 0.5f;
            _image.color = color;
        }

        public void SetCooldown(float cooldown)
        {
            _cooldown = cooldown;
        }

        private void Update()
        {
            if (_image.fillAmount < 1)
            {
                _image.fillAmount = Mathf.Min(_image.fillAmount + Time.deltaTime / _cooldown, 1);
                if (_image.fillAmount == 1)
                {
                    var color = _image.color;
                    color.a = 1;
                    _image.color = color;
                }
            }
        }
    }
}