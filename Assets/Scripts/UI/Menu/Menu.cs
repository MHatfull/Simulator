using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] RectTransform _window;

    public InventorySlot[] InventorySlots { get; private set; }
    public EquipmentSlot WeaponSlot { get; private set; }
    public EquipmentSlot BodySlot { get; private set; }

    private void Awake()
    {
        InventorySlots = Object.FindObjectsOfType<InventorySlot>();
        EquipmentSlot[] equipmentSlots = Object.FindObjectsOfType<EquipmentSlot>();
        WeaponSlot = equipmentSlots.Where(s => s.EquipmentType == EquipmentType.Weapon).First();
        BodySlot = equipmentSlots.Where(s => s.EquipmentType == EquipmentType.Body).First();
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
