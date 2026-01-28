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
    
    private void HandleInteraction()
    {
        DetectInteractable();
        _currentInteractable?.Interact();
        _currentInteractable = null;
    }
    
    private void DetectInteractable()
    {
        if (Physics.SphereCast(rayOrigin.position, sphereCastRadius, rayOrigin.forward, out var hit, interactionRange, interactionLayer))
        {
            _currentInteractable = hit.collider.TryGetComponent(out IInteractable interactable) ? interactable : null;
        }
    }
    
    // Debug SphereCast (ctrl-c ctrl-v)
    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireSphere(transform.position, interactionRange);
    //
    //     RaycastHit hit;
    //     if (Physics.SphereCast(transform.position, sphereCastRadius, transform.forward * interactionRange, out hit, interactionRange, interactionLayer))
    //     {
    //         Gizmos.color = Color.green;
    //         Vector3 sphereCastMidpoint = transform.position + (transform.forward * hit.distance);
    //         Gizmos.DrawWireSphere(sphereCastMidpoint, sphereCastRadius);
    //         Gizmos.DrawSphere(hit.point, 0.1f);
    //         Debug.DrawLine(transform.position, sphereCastMidpoint, Color.green);
    //     }
    //     else
    //     {
    //         Gizmos.color = Color.red;
    //         Vector3 sphereCastMidpoint = transform.position + (transform.forward * (interactionRange-sphereCastRadius));
    //         Gizmos.DrawWireSphere(sphereCastMidpoint, sphereCastRadius);
    //         Debug.DrawLine(transform.position, sphereCastMidpoint, Color.red);
    //     }
    // }
}