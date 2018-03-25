using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class InventoryManager : NetworkBehaviour {
    List<InventorySlot> _inventorySlots;
    EquipmentManager _equipmentManager;

    private void Awake()
    {
        _equipmentManager = GetComponent<EquipmentManager>();
        _inventorySlots = Object.FindObjectOfType<Menu>().InventorySlots.ToList();
        _inventorySlots.ForEach((s) => s.EquipmentManager = _equipmentManager);
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
