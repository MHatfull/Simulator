namespace Underlunchers.Items.Equipment
{

    public enum EquipmentType
    {
        Weapon,
        Body,
        Offhand
    }

    public class Equipment : Collectable
    {
        public float Damage;
        public float DamageReduction;
        public EquipmentType EquipmentType;
    }
}