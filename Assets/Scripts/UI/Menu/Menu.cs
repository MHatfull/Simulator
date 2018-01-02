﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] RectTransform _window;

    void Start()
    {
        InputManager.InventoryToggled += ToggleInventory;
        _window.gameObject.SetActive(false);
    }

    void ToggleInventory()
    {
        _window.gameObject.SetActive(!_window.gameObject.activeSelf);
    }

}