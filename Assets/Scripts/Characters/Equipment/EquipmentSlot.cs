using UnityEngine;

[System.Serializable]
public class EquipmentSlot
{
    public string slotName;
    public Transform socketTransform;
    public IEquippable CurrentItem;
    public ItemData CurrentItemData;
}