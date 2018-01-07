using Simulator.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simulator.UI.Slots
{
    public abstract class EquipmentSlot : ItemSlot
    {
        protected override void OnRightClick()
        {
            Debug.Log("right clicked");
            InventoryManager.AddToInventory(Content);
            Content.gameObject.SetActive(false);
            EmptySlot();
        }

        protected abstract void Unequip();
    }
}