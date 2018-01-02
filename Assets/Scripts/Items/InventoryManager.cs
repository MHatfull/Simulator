using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    static List<InventorySlot> _inventorySlots;

    private void Awake()
    {
        _inventorySlots = FindObjectsOfType<InventorySlot>().ToList();
    }

    public static void AddToInventory(Collectable collectable)
    {
        var freeSlot = _inventorySlots.First(slot => slot.IsEmpty);
        if (freeSlot != null) freeSlot.Add(collectable);
    }
}
