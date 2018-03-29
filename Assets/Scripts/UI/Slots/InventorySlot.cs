using Underlunchers.Items.Equipment;

namespace Underlunchers.UI.Slots
{
    public class InventorySlot : ItemSlot
    {
        public delegate void EquipmentEquippedHanlder(Equipment equipment);
        public EquipmentEquippedHanlder EquipmentEquiped;
        protected override void OnRightClick()
        {
            if (Content is Equipment)
            {
                if (EquipmentEquiped != null)
                {
                    EquipmentEquiped(Content as Equipment);
                }
                EmptySlot();
            }
        }
    }
}