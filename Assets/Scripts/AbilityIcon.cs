using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AbilityIcon : MonoBehaviour {

    Image _image;

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
    }

    public void SetCooldown(float cooldown)
    {
        _cooldown = cooldown;
    }

    private void Update()
    {
        _image.fillAmount = Mathf.Min(_image.fillAmount + Time.deltaTime / _cooldown, 1);
    }
}
