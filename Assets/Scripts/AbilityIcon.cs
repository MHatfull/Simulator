using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AbilityIcon : MonoBehaviour {

    Image _image;

    public KeyCode Key;

    float _cooldown = 1;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetIcon(Sprite icon)
    {
        _image.overrideSprite = icon;
    }

    public void ResetLoadingProgress()
    {
        _image.fillAmount = 0;
        _image.color = new Color(.5f, .5f, .5f, .5f);
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
            if(_image.fillAmount == 1) _image.color = new Color(1, 1, 1, 1);
        }
    }
}
