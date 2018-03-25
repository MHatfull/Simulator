using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerCharacter))]
public class EquipmentManager : NetworkBehaviour {
    WeaponSlot _weaponSlot;
    BodySlot _bodySlot;

    public Weapon Weapon { get; private set; }
    public Body Body { get; private set; }

    private PlayerCharacter _player;
    private InventoryManager _inventory;

    private void Awake()
    {
        if (localPlayerAuthority)
        {
            _weaponSlot = FindObjectOfType<Menu>().WeaponSlot;
            _bodySlot = FindObjectOfType<Menu>().BodySlot;
            _player = GetComponent<PlayerCharacter>();
            _inventory = GetComponent<InventoryManager>();
            _weaponSlot.Inventory = _inventory;
            _bodySlot.Inventory = _inventory;
            _weaponSlot.EquipmentManager = this;
            _bodySlot.EquipmentManager = this;
        }
    }

    public void Equip(Equipment equipment)
    {
        if(equipment is Weapon && Weapon)
        {
            Weapon.NetworkSetActive(false);
        }
        if(equipment is Body && Body)
        {
            Body.NetworkSetActive(false);
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
        if(slot is WeaponSlot)
        {
            CmdUnequipWeapon();
        }
        if(slot is BodySlot)
        {
            CmdUnequipBody();
        }
    }

    [Command]
    void CmdUnequipWeapon()
    {
        if (Weapon)
        {
            Weapon.NetworkSetActive(false);
        }
        Weapon = null;
        RpcUnequipWeapon();
    }

    [ClientRpc]
    void RpcUnequipWeapon()
    {
        Weapon = null;
    }

    [Command]
    void CmdUnequipBody()
    {
        if (Body)
        {
            Body.NetworkSetActive(false);
        }
        Body = null;
        RpcUnequipBody();
    }

    [ClientRpc]
    void RpcUnequipBody()
    {
        Body = null;
    }

    private void SetupEquipment(Equipment equipment)
    {
        equipment.gameObject.SetActive(true);
        equipment.GetComponent<Collider>().enabled = false;
        equipment.transform.SetParent(_player.transform);
        equipment.transform.localEulerAngles = Vector3.forward;
        if (equipment is Weapon)
        {
            Weapon = equipment as Weapon;
            equipment.transform.localPosition = PlayerCharacter.WeaponOffset;
            _weaponSlot.Add(equipment);
        }
        if (equipment is Body)
        {
            Body = equipment as Body;
            equipment.transform.localPosition = Vector3.zero;
            _bodySlot.Add(equipment);
        }
    }

    public float Damage()
    {
        float dmg = 0f;
        dmg += Weapon ? Weapon.Damage : 0;
        dmg += Body ? Body.Damage : 0;
        return dmg;
    }

    public float DamageReduction()
    {
        float dmgReduction = 0f;
        dmgReduction += Weapon ? Weapon.DamageReduction : 0;
        dmgReduction += Body ? Body.DamageReduction : 0;
        return dmgReduction;
    }
}
