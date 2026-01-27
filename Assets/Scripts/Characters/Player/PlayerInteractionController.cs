using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerInteractionController : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionRange = 10f;
    [SerializeField] private float sphereCastRadius = 0.5f;
    [SerializeField] private LayerMask interactionLayer = ~0;
    [SerializeField] private Transform rayOrigin;

    private IInteractable _currentInteractable;
    private PlayerInputController _inputController;

    private void Awake()
    {
        _inputController = GetComponent<PlayerInputController>();
        _inputController.OnInteractEvent += HandleInteraction;
        
        if (rayOrigin == null)
            if (Camera.main != null) rayOrigin = Camera.main.transform;
    }

    private void Update()
    {
        Debug.DrawRay(rayOrigin.position, rayOrigin.forward * interactionRange, Color.red);
    }

    private void HandleInteraction()
    {
        DetectInteractable();
        _currentInteractable?.Interact();
        _currentInteractable = null;
    }
    
    private void DetectInteractable()
    {
        if (Physics.SphereCast(rayOrigin.position, sphereCastRadius, rayOrigin.forward, out RaycastHit hit, interactionRange, interactionLayer))
        {
            _currentInteractable = hit.collider.TryGetComponent(out IInteractable interactable) ? interactable : null;
        }
    }
}