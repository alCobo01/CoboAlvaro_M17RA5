using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerInteractionController : MonoBehaviour
{
    [Header("UI Indicators")] 
    [SerializeField] private GameObject uiObject;
    [SerializeField] private TMP_Text uiText;
    [SerializeField] private string baseText = "Press E to ";
    
    [Header("Interaction Settings")]
    [SerializeField] private float interactionRange = 10f;
    [SerializeField] private float sphereCastRadius = 5f;
    [SerializeField] private LayerMask interactionLayer = ~0;
    [SerializeField] private Transform rayOrigin;

    private IInteractable _currentInteractable;
    private PlayerInputController _inputController;

    private void Awake()
    {
        _inputController = GetComponent<PlayerInputController>();
        _inputController.OnInteractEvent += HandleInteraction;
    }

    private void Update()
    {
        DetectInteractable();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_currentInteractable != null)
        {
            uiText.text = baseText + _currentInteractable.InteractionPrompt;
            uiObject.gameObject.SetActive(true);
        }
        else uiObject.gameObject.SetActive(false);
    }

    private void HandleInteraction() => _currentInteractable?.Interact(gameObject);

    private void DetectInteractable()
    {
        var offsetOrigin = rayOrigin.position - (rayOrigin.forward * sphereCastRadius);
        var adjustedRange = interactionRange * sphereCastRadius;
        
        if (Physics.SphereCast(offsetOrigin, sphereCastRadius, rayOrigin.forward, out var hit, adjustedRange, interactionLayer))
        {
            _currentInteractable = hit.collider.TryGetComponent(out IInteractable interactable) ? interactable : null;
        }
        else _currentInteractable = null;
    }
}