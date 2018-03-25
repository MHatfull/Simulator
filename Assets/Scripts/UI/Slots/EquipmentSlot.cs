using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentSlot : ItemSlot
{
    public InventoryManager Inventory;
    public EquipmentManager EquipmentManager;

    protected override void OnRightClick()
    {
        if (Content)
        {
            EquipmentManager.Unequip(this);
            Inventory.AddToInventory(Content);
            EmptySlot();
        }
    }

    protected abstract void Unequip();
}
