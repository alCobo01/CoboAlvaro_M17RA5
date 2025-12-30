using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CrouchBehaviour : MonoBehaviour
{
    [SerializeField] private float crouchHeight = 1f;
    [SerializeField] private Vector3 crouchCenter = new Vector3(0, 0.5f, 0);

    private float _originalHeight;
    private Vector3 _originalCenter;
    private CapsuleCollider _collider;

    public bool IsCrouching { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider>();
        _originalHeight = _collider.height;
        _originalCenter = _collider.center;
    }

    public void ToggleCrouch(bool shouldCrouch)
    {
        IsCrouching = shouldCrouch;

        if (shouldCrouch)
        {
            _collider.height = crouchHeight;
            _collider.center = crouchCenter;
        }
        else
        {
            _collider.height = _originalHeight;
            _collider.center = _originalCenter;
        }
    }
}
