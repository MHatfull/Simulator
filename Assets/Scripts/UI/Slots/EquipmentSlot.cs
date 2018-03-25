using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentSlot : ItemSlot
{
    public InventoryManager Inventory;

    protected override void OnRightClick()
    {
        Debug.Log("right clicked");
        Inventory.AddToInventory(Content);
        Content.gameObject.SetActive(false);
        EmptySlot();
    }

    protected abstract void Unequip();
}
