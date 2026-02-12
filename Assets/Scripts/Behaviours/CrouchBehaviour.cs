using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CrouchBehaviour : MonoBehaviour
{
    [SerializeField] private float crouchHeight = 1f;
    [SerializeField] private Vector3 crouchCenter = new Vector3(0, 0.5f, 0);

    private float _originalHeight;
    private Vector3 _originalCenter;
    private CharacterController _controller;

    public bool IsCrouching { get; private set; }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _originalHeight = _controller.height;
        _originalCenter = _controller.center;
    }

    public void ToggleCrouch(bool shouldCrouch)
    {
        IsCrouching = shouldCrouch;

        if (shouldCrouch)
        {
            _controller.height = crouchHeight;
            _controller.center = crouchCenter;
        }
        else
        {
            _controller.height = _originalHeight;
            _controller.center = _originalCenter;
        }
    }
}
