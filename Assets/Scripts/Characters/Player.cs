using static InputSystem_Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : Character, IPlayerActions
{
    [Header("Camera settings")]
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float mouseSensitivity = 0.5f;
    [SerializeField] private float minPitch = -10f;
    [SerializeField] private float maxPitch = 10f;

    [Header("Movement speed values")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 7f;

    private InputSystem_Actions _inputActions;
    private Vector3 _moveInput;
    private Vector2 _lookInput;
    private float _currentSpeed, _pitch;
    private bool _isSprinting;

    private void Awake()
    {
        moveBehaviour = GetComponent<MoveBehaviour>();
        animationBehaviour = GetComponent<AnimationBehaviour>();

        _inputActions = new InputSystem_Actions();
        _inputActions.Player.SetCallbacks(this);

        _currentSpeed = walkSpeed;

        Application.targetFrameRate = 60;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start() => _inputActions.Enable();
    void OnEnable() => _inputActions.Enable();
    void OnDisable() => _inputActions.Disable();

    private void Update()
    {
        float yawDelta = _lookInput.x * mouseSensitivity;
        transform.Rotate(Vector3.up * yawDelta);

        _pitch -= _lookInput.y * mouseSensitivity;
        _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);

        cameraPivot.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }

    private void FixedUpdate() => moveBehaviour.Move(_moveInput, _currentSpeed);

    public void OnAttack(InputAction.CallbackContext context)
    {
        
    }

    public void OnDance(InputAction.CallbackContext context) => animationBehaviour.PlayDance();

    public void OnJump(InputAction.CallbackContext context)
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        _moveInput = new Vector3(input.x, 0, input.y);
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        _isSprinting = context.ReadValueAsButton();
        _currentSpeed = _isSprinting ? runSpeed : walkSpeed;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }
}
