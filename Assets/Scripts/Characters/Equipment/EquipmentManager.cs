using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public List<EquipmentSlot> slots = new();

    public void EquipItem(ItemData data)
    {
        var slot = slots.Find(s => s.slotName == data.slotName);

        slot.CurrentItem?.Unequip();

        var instance = Instantiate(data.prefab);
        var equippable = instance.GetComponent<IEquippable>();

        if (equippable == null) return;
        equippable.Equip(slot.socketTransform);
        slot.CurrentItem = equippable;
        slot.currentItemData = data;
    }

    public void Unequip(string slotName)
    {
        var slot = slots.Find(s => s.slotName == slotName);

        if (slot?.CurrentItem == null) return;
        slot.CurrentItem.Unequip();
        slot.CurrentItem = null;
        slot.currentItemData = null;
    }

    public List<string> GetEquippedItemNames()
    {
        return (from slot in slots where slot.currentItemData != null select slot.currentItemData.itemName).ToList();
    }
}