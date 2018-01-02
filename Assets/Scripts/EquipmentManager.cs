using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
    static WeaponSlot _weaponSlot;
    static BodySlot _bodySlot;

    private void Awake()
    {
        _weaponSlot = FindObjectOfType<WeaponSlot>();
        _bodySlot = FindObjectOfType<BodySlot>();
    }

    public static void Equip(Equipment equipment)
    {
        if(equipment is Weapon)
        {
            _weaponSlot.Add(equipment);
        }
        if(equipment is Body)
        {
            _bodySlot.Add(equipment);
        }
    }

    public static float Damage()
    {
        float dmg = 0f;
        dmg += _weaponSlot.Content ? ((Equipment)_weaponSlot.Content).Damage : 0;
        dmg += _bodySlot.Content ? ((Equipment)_bodySlot.Content).Damage : 0;
        return dmg;
    }

    public static float DamageReduction()
    {
        float dmgReduction = 0f;
        dmgReduction += _weaponSlot.Content ? ((Equipment)_weaponSlot.Content).DamageReduction : 0;
        dmgReduction += _bodySlot.Content ? ((Equipment)_bodySlot.Content).DamageReduction : 0;
        return dmgReduction;
    }
}
