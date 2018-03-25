﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerCharacter))]
public class EquipmentManager : NetworkBehaviour {
    public Dictionary<EquipmentType, EquipmentSlot> EquipmentSlots = new Dictionary<EquipmentType, EquipmentSlot>();

    public Dictionary<EquipmentType, Equipment> Equipped = new Dictionary<EquipmentType, Equipment>();

    private PlayerCharacter _player;
    private InventoryManager _inventory;

    private void Awake()
    {
        if (localPlayerAuthority)
        {
            EquipmentSlots.Add(EquipmentType.Weapon, FindObjectOfType<Menu>().WeaponSlot);
            EquipmentSlots.Add(EquipmentType.Body, FindObjectOfType<Menu>().BodySlot);
            _player = GetComponent<PlayerCharacter>();
            _inventory = GetComponent<InventoryManager>();
            foreach (EquipmentSlot slot in EquipmentSlots.Values)
            {
                slot.EquipmentUnequipped += Unequip;
            }
        }
    }

    public void Equip(Equipment equipment)
    {
        if (equipment)
        {
            equipment.NetworkSetActive(false);
        }
        CmdEquip(equipment.netId);
    }

    [Command]
    private void CmdEquip(NetworkInstanceId id)
    {
        RpcEquip(id);
        GameObject equipment = NetworkServer.FindLocalObject(id);
        SetupEquipment(equipment.GetComponent<Equipment>());
    }

    [ClientRpc]
    private void RpcEquip(NetworkInstanceId id)
    {
        GameObject equipment = ClientScene.FindLocalObject(id);
        SetupEquipment(equipment.GetComponent<Equipment>());
    }

    public void Unequip(EquipmentSlot slot)
    {
        _inventory.AddToInventory(slot.Content);
        CmdUnequip(slot.EquipmentType);
    }

    [Command]
    void CmdUnequip(EquipmentType type)
    {
        if (Equipped[type] != null)
        {
            Equipped[type].NetworkSetActive(false);
        }
        Equipped[type] = null;
        RpcUnequip(type);
    }

    [ClientRpc]
    void RpcUnequip(EquipmentType type)
    {
        Equipped[type] = null;
    }

    private void SetupEquipment(Equipment equipment)
    {
        equipment.gameObject.SetActive(true);
        equipment.GetComponent<Collider>().enabled = false;
        equipment.transform.SetParent(_player.transform);
        equipment.transform.localEulerAngles = Vector3.forward;
        if (Equipped.ContainsKey(equipment.EquipmentType))
        {
            Equipped[equipment.EquipmentType] = equipment;
        }
        else
        {
            Equipped.Add(equipment.EquipmentType, equipment);
        }
        EquipmentSlots[equipment.EquipmentType].Add(equipment);
        switch (equipment.EquipmentType)
        {
            case (EquipmentType.Weapon):
                equipment.transform.localPosition = PlayerCharacter.WeaponOffset;
                break;
            case (EquipmentType.Body):
                equipment.transform.localPosition = Vector3.zero;
                break;
        }
    }

    public float Damage()
    {
        float dmg = 0f;
        foreach (Equipment equipment in Equipped.Values)
        {
            dmg += equipment.Damage;
        }
        return dmg;
    }

    public float DamageReduction()
    {
        float dmgReduction = 0f;
        foreach (Equipment equipment in Equipped.Values)
        {
            dmgReduction += equipment.DamageReduction;
        }
        return dmgReduction;
    }
}
