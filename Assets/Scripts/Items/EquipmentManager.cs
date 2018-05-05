using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Underlunchers.Items.Equipment
{
    [RequireComponent(typeof(InventoryManager))]
    public class EquipmentManager : NetworkBehaviour, IEnumerable<Equipment>
    {
        public delegate void EquipmentUpdatedHandler();
        public event EquipmentUpdatedHandler EquipmentUpdated;

        InventoryManager _inventory;

        private void Awake()
        {
            _inventory = GetComponent<InventoryManager>();
        }

        public void Equip(Equipment equipment)
        {
            if (this[equipment.EquipmentType]) Unequip(equipment.EquipmentType);
            if (!(isServer && isLocalPlayer))
            {
                if (isServer)      RpcEquip(equipment.netId);
                else if (isClient) CmdEquip(equipment.netId);
            }
            else
            {
                RpcEquip(equipment.netId);
            }
            LocalEquip(equipment);
        }

        [Command]
        private void CmdEquip(NetworkInstanceId id)
        {
            GameObject equipment = NetworkServer.FindLocalObject(id);
            LocalEquip(equipment.GetComponent<Equipment>());
        }

        [ClientRpc]
        private void RpcEquip(NetworkInstanceId id)
        {
            GameObject equipment = ClientScene.FindLocalObject(id);
            LocalEquip(equipment.GetComponent<Equipment>());
        }

        private void LocalEquip(Equipment equipment)
        {
            this[equipment.EquipmentType] = equipment;
            equipment.gameObject.SetActive(true);
            equipment.GetComponent<Collider>().enabled = false;
            equipment.transform.SetParent(transform);
            equipment.transform.localEulerAngles = Vector3.zero;
            equipment.transform.localPosition = Vector3.zero;
            IssueEquipmentUpdated();
        }

        public void Unequip(EquipmentType type)
        {
            _inventory.Add(this[type]);
            if (!(isServer && isLocalPlayer))
            {
                if (isServer)      RpcUnequip(type);
                else if (isClient) CmdUnequip(type);
            } else
            {
                RpcUnequip(type);
            }
            LocalUnequip(type);
        }

        [Command]
        void CmdUnequip(EquipmentType type)
        {
            LocalUnequip(type);
        }

        [ClientRpc]
        void RpcUnequip(EquipmentType type)
        {
            LocalUnequip(type);
        }

        private void LocalUnequip(EquipmentType type)
        {
            if (this[type] != null)
            {
                this[type].gameObject.SetActive(false);
            }
            this[type] = null;
            IssueEquipmentUpdated();
        }

        private void IssueEquipmentUpdated()
        {
            if (EquipmentUpdated != null) EquipmentUpdated();
        }

        public float Damage()
        {
            float dmg = 0f;
            foreach (Equipment equipment in this)
            {
                dmg += equipment.Damage;
            }
            return dmg;
        }

        public float DamageReduction()
        {
            float dmgReduction = 0f;
            foreach (Equipment equipment in this)
            {
                dmgReduction += equipment.DamageReduction;
            }
            return dmgReduction;
        }

        private Dictionary<EquipmentType, Equipment> _equipped = new Dictionary<EquipmentType, Equipment>();

        public IEnumerator<Equipment> GetEnumerator()
        {
            return _equipped.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Equipment this[EquipmentType type]
        {
            get
            {
                if (_equipped.ContainsKey(type))
                {
                    return _equipped[type];
                }
                else
                {
                    return null;
                }
            }
            private set
            {
                if (_equipped.ContainsKey(type))
                {
                    _equipped[type] = value;
                }
                else
                {
                    _equipped.Add(type, value);
                }
            }
        }
    }
}