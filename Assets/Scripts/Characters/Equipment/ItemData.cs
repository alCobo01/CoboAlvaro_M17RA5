using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public GameObject prefab;
    public string slotName;
}
