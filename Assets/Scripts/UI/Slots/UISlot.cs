using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public abstract class UISlot : MonoBehaviour
{
    protected Image Image
    {
        get
        {
            if (!_image) _image = GetComponent<Image>();
            return _image;
        }
    }
    private Image _image;

    public void SetIcon(Sprite icon)
    {
        Image.overrideSprite = icon;
    }
}
