using UnityEngine;

public class PlayerCharacter : Character
{
    public static PlayerCharacter Me;
    public static readonly Vector3 WeaponOffset = Vector3.forward;
    protected override void Awake()
    {
        base.Awake();
        Me = this;
    }

    public override Vector3 FoculPoint
    {
        get
        {
            return Camera.main.transform.position;
        }
    }

    public override Vector3 AimDirection()
    {
        return Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1)) - Camera.main.transform.position;
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
        EquipmentManager.Weapon.Swing();
    }
}
