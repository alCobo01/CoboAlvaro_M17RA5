using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EquipmentManager))]
public class PlayerEquipmentController : MonoBehaviour
{
    [SerializeField] private List<ItemData> startingItems = new List<ItemData>();
    private EquipmentManager _equipmentManager;

    private void Awake()
    {
        _equipmentManager = GetComponent<EquipmentManager>();
    }

    private void Start()
    {
        foreach (var item in startingItems) Equip(item);
    }

    public void Equip(ItemData itemData)
    {
        if (itemData == null) return;
        _equipmentManager.EquipItem(itemData);
    }

    public void Unequip(string slotName)
    {
        if (string.IsNullOrEmpty(slotName)) return;
        _equipmentManager.Unequip(slotName);
    }
}