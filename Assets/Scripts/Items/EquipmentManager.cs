using Simulator.Characters.Player;
using Simulator.UI.Slots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simulator.Items
{
    public class EquipmentManager : MonoBehaviour
    {
        static WeaponSlot _weaponSlot;
        static BodySlot _bodySlot;

        public static Weapon Weapon { get; private set; }

        private void Awake()
        {
            _weaponSlot = FindObjectOfType<WeaponSlot>();
            _bodySlot = FindObjectOfType<BodySlot>();
        }

        public static void Equip(Equipment equipment)
        {
            equipment.gameObject.SetActive(true);
            equipment.GetComponent<Collider>().enabled = false;
            equipment.transform.SetParent(PlayerCharacter.Me.transform);
            if (equipment is Weapon)
            {
                Weapon = equipment as Weapon;
                equipment.transform.localPosition = PlayerCharacter.WeaponOffset;
                Debug.Log("local pos is " + equipment.transform.localPosition);
                _weaponSlot.Add(equipment);
            }
            if (equipment is Body)
            {
                equipment.transform.localPosition = Vector3.zero;
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
}