﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public abstract class UISlot : MonoBehaviour
{
    protected Image _image;

    protected virtual void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetIcon(Sprite icon)
    {
        _image.overrideSprite = icon;
    }
}
