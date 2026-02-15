using UnityEngine;
using UnityEngine.Events;

public class Campfire : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt = "save game";
    [SerializeField] private UnityEvent onInteracted;

    public string InteractionPrompt => prompt;

    public void Interact(GameObject interactor)
    {
        GameManager.Instance.SavePlayerProgress();
        onInteracted?.Invoke();
    }
}