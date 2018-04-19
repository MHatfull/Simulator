using Underlunchers.Input;
using Underlunchers.Items;
using Underlunchers.Items.Equipment;
using Underlunchers.UI.Slots;
using Underlunchers.Characters;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

namespace Underlunchers.UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] RectTransform _window;
        [SerializeField] Dropdown _dropdown;

        private EquipmentManager _equipmentManager;
        private InventoryManager _inventoryManager;

        private InventorySlot[] _inventorySlots;
        private EquipmentSlot[] _equipmentSlots; 

        private void Awake()
        {
            var found = GameObject.FindObjectsOfType<InventorySlot>().ToList();
            found.Sort((a, b) => (int)(b.transform.position.y - a.transform.position.y) * 100 + (int)(a.transform.position.x - b.transform.position.x));
            _inventorySlots = found.ToArray();
            foreach (InventorySlot slot in _inventorySlots)
            {
                slot.OnRightClicked += OnInventorySlotClicked;
            }
            _equipmentSlots = GameObject.FindObjectsOfType<EquipmentSlot>();
            foreach (EquipmentSlot slot in _equipmentSlots)
            {
                slot.OnRightClicked += OnEquipmentSlotClicked;
            }
            InputManager.InventoryToggled += ToggleMenu;
            _window.gameObject.SetActive(false);
            Character.OnSelfJoined += OnJoined;
        }

        private void OnEquipmentSlotClicked(Collectable collectable)
        {
            _equipmentManager.Unequip(((Equipment)collectable).EquipmentType);
        }

        private void OnInventorySlotClicked(Collectable collectable)
        {
            if (collectable is Equipment)
            {
                _equipmentManager.Equip((Equipment)collectable);
                _inventoryManager.Remove(collectable);
            }
        }

        void OnJoined(Character me)
        {
            _inventoryManager = me.GetComponent<InventoryManager>();
            _equipmentManager = me.GetComponent<EquipmentManager>();
            _inventoryManager.InventoryUpdated += UpdateInventory;
            _equipmentManager.EquipmentUpdated += UpdateEquipment;
        }

        private void UpdateEquipment()
        {
            for (int i = 0; i < _equipmentSlots.Length; i++)
            {
                Equipment e = _equipmentManager[_equipmentSlots[i].EquipmentType];
                if (e)
                {
                    _equipmentSlots[i].SetContent(e);
                }
                else
                {
                    _equipmentSlots[i].SetEmpty();
                }
            }
        }

        private void UpdateInventory()
        {
            for (int i = 0; i < _inventorySlots.Length; i++)
            {
                if (i < _inventoryManager.Length)
                {
                    Collectable c = _inventoryManager[i];
                    _inventorySlots[i].SetContent(c);
                } else
                {
                    _inventorySlots[i].SetEmpty();
                }
            }
        }

        void ToggleMenu()
        {
            _window.gameObject.SetActive(!_window.gameObject.activeSelf);
        }

    }
}