using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] RectTransform _window;

    public InventorySlot[] InventorySlots { get; private set; }
    public WeaponSlot WeaponSlot { get; private set; }
    public BodySlot BodySlot { get; private set; }

    private void Awake()
    {
        InventorySlots = FindObjectsOfType<InventorySlot>();
        BodySlot = FindObjectOfType<BodySlot>();
        WeaponSlot = FindObjectOfType<WeaponSlot>();
        Debug.Log("weapon slot is " + WeaponSlot);
    }

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
