public class InventorySlot : ItemSlot
{
    public EquipmentManager EquipmentManager;

    protected override void OnRightClick()
    {
        if(Content is Equipment)
        {
            EquipmentManager.Equip((Equipment)Content);
            EmptySlot();
        } 
    }
}
