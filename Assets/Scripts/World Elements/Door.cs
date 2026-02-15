using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string InteractionPrompt => IsOpen ? "close the door" : "open the door" ;
    public bool IsOpen { get; private set; }

    [Header("Rotation config")] 
    [SerializeField] private float rotationAmount = 90f;
    [SerializeField] private float forwardDirection = 0f;
    [SerializeField] private float rotationDuration = 1f;
    [SerializeField] private float rotationSpeed = 1f;

    private Vector3 _startRotation, _forward;
    private Coroutine _animationCoroutine;

    private void Awake()
    {
        _startRotation = transform.rotation.eulerAngles;
        _forward = transform.right;
        IsOpen = false;
    }
    
    //Open door method and coroutine
    private void Open(Vector3 userPosition)
    {
        if (IsOpen) return;
        if (_animationCoroutine != null)
            StopCoroutine(_animationCoroutine);

        //Calculate if player is in front or behind the door
        var dot = Vector3.Dot(_forward, (userPosition - transform.position).normalized);
        _animationCoroutine = StartCoroutine(DoRotationOpen(dot));
    }
    
    private IEnumerator DoRotationOpen(float forwardAmount)
    {
        var startRotation = transform.rotation;
        Quaternion endRotation;

        // Decide which way the door opens
        if (forwardAmount >= forwardDirection)
            endRotation = Quaternion.Euler(new Vector3(0, _startRotation.y - rotationAmount, 0));
        else
            endRotation = Quaternion.Euler(new Vector3(0, _startRotation.y + rotationAmount, 0));

        IsOpen = true;
        var time = 0f;

        while (time < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * rotationSpeed;
        }
    }
    
    //Close door method and coroutine
    private void Close()
    {
        if (IsOpen)
        {
            if (_animationCoroutine != null)
                StopCoroutine(_animationCoroutine);

            _animationCoroutine = StartCoroutine(DoRotationClose());
        }
    }

    private IEnumerator DoRotationClose()
    {
        var startRotation = transform.rotation;
        var endRotation = Quaternion.Euler(_startRotation);

        IsOpen = false;
        var time = 0f;
        
        while (time < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * rotationSpeed;
        }
    }
    
    public void Interact(GameObject interactor)
    {
        if (IsOpen)
        {
            Close();
            return;
        }

        if (!interactor.TryGetComponent<EquipmentManager>(out var equipmentManager)) return;
        var equippedItems = equipmentManager.GetEquippedItemNames();
        
        if (equippedItems.Contains("Key")) Open(interactor.transform.position);
        else
        {
            if (interactor.TryGetComponent<PlayerInteractionController>(out var interactionController))
            {
                interactionController.ShowTemporaryMessage("You need a key to open this door!", 2f);
            }
        }
    }
}
