using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
    static WeaponSlot _weaponSlot;

    private void Awake()
    {
        _weaponSlot = FindObjectOfType<WeaponSlot>();
        Debug.Log("weapon slot is " + _weaponSlot);
    }

    public static void Equip(Equipment equipment)
    {
        if(equipment is Weapon)
        {
            _weaponSlot.Add(equipment);
        }
    }
}
