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
        }
    }

    public void Equip(Equipment equipment)
    {
        CmdEquip(equipment.netId);
    }

    [Command]
    void CmdEquip(NetworkInstanceId id)
    {
        RpcEquip(id);
        GameObject equipment = NetworkServer.FindLocalObject(id);
        SetupEquipment(equipment.GetComponent<Equipment>());
    }

    [ClientRpc]
    void RpcEquip(NetworkInstanceId id)
    {
        GameObject equipment = ClientScene.FindLocalObject(id);
        SetupEquipment(equipment.GetComponent<Equipment>());
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
