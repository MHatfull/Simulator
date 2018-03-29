using Underlunchers.Input;
using Underlunchers.UI.Slots;
using UnityEngine;

namespace Underlunchers.UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] RectTransform _window;

        public InventorySlot[] InventorySlots { get; private set; }
        public EquipmentSlot[] EquipmentSlots { get; private set; }

        private void Awake()
        {
            InventorySlots = Object.FindObjectsOfType<InventorySlot>();
            EquipmentSlots = Object.FindObjectsOfType<EquipmentSlot>();
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
}