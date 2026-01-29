using TMPro;
using Unity.Hierarchy;
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
        if (Physics.SphereCast(rayOrigin.position, sphereCastRadius, rayOrigin.forward, out var hit, interactionRange, interactionLayer))
        {
            Debug.Log(hit.collider.name);
            _currentInteractable = hit.collider.TryGetComponent(out IInteractable interactable) ? interactable : null;
            //if (interactable == null) hit.collider.GetComponentInParent<IInteractable>();
        }
        else _currentInteractable = null;
    }
    
    // Debug SphereCast (ctrl-c ctrl-v)
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRange);

        if (Physics.SphereCast(transform.position, sphereCastRadius, transform.forward * interactionRange, out var hit, interactionRange, interactionLayer))
        {
            Gizmos.color = Color.green;
            Vector3 sphereCastMidpoint = transform.position + (transform.forward * hit.distance);
            Gizmos.DrawWireSphere(sphereCastMidpoint, sphereCastRadius);
            Gizmos.DrawSphere(hit.point, 0.1f);
            Debug.DrawLine(transform.position, sphereCastMidpoint, Color.green);
        }
        else
        {
            Gizmos.color = Color.red;
            Vector3 sphereCastMidpoint = transform.position + (transform.forward * (interactionRange-sphereCastRadius));
            Gizmos.DrawWireSphere(sphereCastMidpoint, sphereCastRadius);
            Debug.DrawLine(transform.position, sphereCastMidpoint, Color.red);
        }
    }
}