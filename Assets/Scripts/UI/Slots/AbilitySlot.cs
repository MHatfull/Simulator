using UnityEngine;

namespace Underlunchers.UI.Slots
{
    public class AbilitySlot : UISlot
    {
        public KeyCode Key;

        float _cooldown = 1;

        public void ResetLoadingProgress()
        {
            Image.fillAmount = 0;
            Image.color = new Color(.5f, .5f, .5f, .5f);
        }

        public void SetCooldown(float cooldown)
        {
            _cooldown = cooldown;
        }

        private void Update()
        {
            if (Image.fillAmount < 1)
            {
                Image.fillAmount = Mathf.Min(Image.fillAmount + Time.deltaTime / _cooldown, 1);
                if (Image.fillAmount == 1) Image.color = new Color(1, 1, 1, 1);
            }
        }
    }
}