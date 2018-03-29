using System.Collections.Generic;
using System.Linq;
using Underlunchers.Items.Equipment;
using Underlunchers.UI;
using Underlunchers.UI.Slots;
using UnityEngine;
using UnityEngine.Networking;

namespace Underlunchers.Items
{
    public class InventoryManager : NetworkBehaviour
    {
        List<InventorySlot> _inventorySlots;
        EquipmentManager _equipmentManager;

        private void Awake()
        {
            _equipmentManager = GetComponent<EquipmentManager>();
            _inventorySlots = FindObjectOfType<Menu>().InventorySlots.ToList();
            _inventorySlots.ForEach((s) => s.EquipmentEquiped += _equipmentManager.Equip);
        }

        [ClientRpc]
        public void RpcAddToInventory(NetworkInstanceId id)
        {
            GameObject collectable = ClientScene.FindLocalObject(id);
            AddToInventory(collectable.GetComponent<Collectable>());
        }

        public void AddToInventory(Collectable collectable)
        {
            var freeSlot = _inventorySlots.First(slot => slot.IsEmpty);
            if (freeSlot != null) freeSlot.Add(collectable);
        }
    }
}