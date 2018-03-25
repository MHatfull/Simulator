using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(EquipmentManager))]
[RequireComponent(typeof(InventoryManager))]
public class PlayerCharacter : Character
{
    public static readonly Vector3 WeaponOffset = Vector3.forward;

    EquipmentManager _equipmentManager;
    public InventoryManager Inventory;
    protected override void Awake()
    {
        base.Awake();
        _equipmentManager = GetComponent<EquipmentManager>();
        Inventory = GetComponent<InventoryManager>();
    }

    [SyncVar]
    Vector3 _focalPoint;

    public override Vector3 FoculPoint
    {
        get
        {
            return _focalPoint;
        }
    }

    [SyncVar]
    Vector3 _aimDirection;

    public override Vector3 AimDirection()
    {
        return _aimDirection;
    }

    public void UpdateFocus(Vector3 point, Vector3 direction)
    {
        _focalPoint = point;
        _aimDirection = direction;
    }

    public override void DealDamage(float damage, Character source)
    {
        base.DealDamage(Mathf.Clamp(damage - _equipmentManager.DamageReduction(), 0, Mathf.Infinity), source);
    }

    internal override float DamageBonus()
    {
        return _equipmentManager.Damage();
    }

    public override void PlayWeaponAttackAnimation()
    {
        Debug.LogWarning("Cannot play weapon attack");
    }
}
