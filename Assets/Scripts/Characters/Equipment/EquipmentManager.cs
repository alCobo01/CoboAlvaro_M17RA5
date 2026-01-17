using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public List<EquipmentSlot> slots = new List<EquipmentSlot>();

    public void EquipItem(ItemData data)
    {
        EquipmentSlot slot = slots.Find(s => s.slotName == data.slotName);

        if (slot.currentItem != null) slot.currentItem.Unequip();

        var instance = Instantiate(data.prefab);
        var equippable = instance.GetComponent<IEquippable>();

        if (equippable != null)
        {
            equippable.Equip(slot.socketTransform);
            slot.currentItem = equippable;
        }
    }
}