using UnityEngine;
using UnityEngine.Networking;

public class PlayerCharacter : Character
{
    public static PlayerCharacter Me;
    public static readonly Vector3 WeaponOffset = Vector3.forward;
    protected override void Awake()
    {
        base.Awake();
        Me = this;
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
        base.DealDamage(Mathf.Clamp(damage - EquipmentManager.DamageReduction(), 0, Mathf.Infinity), source);
    }

    internal override float DamageBonus()
    {
        return EquipmentManager.Damage();
    }

    public override void PlayWeaponAttackAnimation()
    {
        if(EquipmentManager.Weapon) EquipmentManager.Weapon.Swing();
    }
}
