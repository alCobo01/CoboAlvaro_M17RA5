using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData itemData;

    public void Interact()
    {
        if (itemData == null) return;
        
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && player.TryGetComponent(out PlayerEquipmentController equipmentController))
        {
            equipmentController.Equip(itemData);
            Destroy(gameObject);
        }
    }
}
