using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    public EquipmentType EquipmentType;

    public delegate void EquipmentUnequippedHandler(EquipmentSlot slot);
    public event EquipmentUnequippedHandler EquipmentUnequipped;

    protected override void OnRightClick()
    {
        if (Content)
        {
            if (EquipmentUnequipped != null)
            {
                EquipmentUnequipped(this);
            }
            EmptySlot();
        }
    }
}
