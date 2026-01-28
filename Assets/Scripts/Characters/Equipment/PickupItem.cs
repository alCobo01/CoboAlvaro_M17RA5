using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData itemData;

    public string InteractionPrompt => itemData != null ? $"pick up the {itemData.itemName}" : "pick up the item";

    public void Interact(GameObject interactor)
    {
        if (itemData == null) return;

        if (interactor != null && interactor.TryGetComponent(out PlayerEquipmentController equipmentController))
        {
            equipmentController.Equip(itemData);
            Destroy(gameObject);
        }
    }
}
