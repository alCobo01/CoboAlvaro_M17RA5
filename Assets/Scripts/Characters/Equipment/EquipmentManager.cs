using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public List<EquipmentSlot> slots = new List<EquipmentSlot>();

    public void EquipItem(ItemData data)
    {
        EquipmentSlot slot = slots.Find(s => s.slotName == data.slotName);

        slot.CurrentItem?.Unequip();

        var instance = Instantiate(data.prefab);
        var equippable = instance.GetComponent<IEquippable>();

        if (equippable == null) return;
        equippable.Equip(slot.socketTransform);
        slot.CurrentItem = equippable;
    }

    public void Unequip(string slotName)
    {
        var slot = slots.Find(s => s.slotName == slotName);

        if (slot?.CurrentItem == null) return;
        slot.CurrentItem.Unequip();
        slot.CurrentItem = null;
    }
}