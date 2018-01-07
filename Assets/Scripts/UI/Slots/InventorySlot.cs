using Simulator.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simulator.UI.Slots
{
    public class InventorySlot : ItemSlot
    {
        protected override void OnRightClick()
        {
            if (Content is Equipment)
            {
                EquipmentManager.Equip((Equipment)Content);
                EmptySlot();
            }
        }
    }
}