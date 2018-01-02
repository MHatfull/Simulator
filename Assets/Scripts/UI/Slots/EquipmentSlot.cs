using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    protected override void OnRightClick()
    {
        Debug.Log("right clicked");
        InventoryManager.AddToInventory(Content);
        EmptySlot();
    }
}
