using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(JumpBehaviour))]
[RequireComponent(typeof(CrouchBehaviour))]
public class PlayerMovementController : Character
{
    [Header("Dependencies")]
    [SerializeField] private PlayerInputController inputController;
    [SerializeField] private Transform cameraTransform;

    [Header("Movement speed values")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float crouchSpeed = 2.5f;

    private JumpBehaviour _jumpBehaviour;
    private CrouchBehaviour _crouchBehaviour;
    private Vector3 _moveInput;
    private float _currentSpeed;
    private bool _isSprinting, _wantsToCrouch;

    private void Awake()
    {
        moveBehaviour = GetComponent<MoveBehaviour>();
        animationBehaviour = GetComponent<AnimationBehaviour>();
        inputController = GetComponent<PlayerInputController>();
        _jumpBehaviour = GetComponent<JumpBehaviour>();
        _crouchBehaviour = GetComponent<CrouchBehaviour>();
        _currentSpeed = walkSpeed;

        // Subscribe to input events
        inputController.OnMoveEvent += HandleMove;
        inputController.OnSprintEvent += HandleSprint;
        inputController.OnDanceEvent += HandleDance;
        inputController.OnJumpEvent += HandleJump;
        inputController.OnCrouchEvent += HandleCrouch;
    }

    private void Update()
    {
        // Update animation grounded state and vertical velocity
        animationBehaviour.SetGrounded(_jumpBehaviour.IsGrounded);
        animationBehaviour.SetVerticalVelocity(_jumpBehaviour.VerticalVelocity);

    }

    private void FixedUpdate() 
    {
        RotatePlayer();
        moveBehaviour.Move(_moveInput, _currentSpeed);
    }

    private void RotatePlayer()
    {
        // Only rotate if we are actually moving
        if (_moveInput.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void HandleMove(Vector2 input)
    {
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        // Flatten Y (to not walk into the ground/sky)
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // Create Camera-Relative Movement Vector
        _moveInput = (camForward * input.y) + (camRight * input.x);
    }

    private void HandleSprint(bool isSprinting)
    {
        _isSprinting = isSprinting;
        UpdateSpeed();
    }

    private void HandleDance() => animationBehaviour.PlayDance();

    private void HandleJump() 
    {
        if (_jumpBehaviour.IsGrounded)
        {
            _jumpBehaviour.Jump();
            animationBehaviour.TriggerJump();
        }
    }

    private void HandleCrouch(bool isCrouching)
    {
        _wantsToCrouch = isCrouching;
        _crouchBehaviour.ToggleCrouch(_wantsToCrouch);
        animationBehaviour.SetCrouch(_wantsToCrouch);
        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        if 
            (_crouchBehaviour.IsCrouching) _currentSpeed = crouchSpeed;
        else if 
            (_isSprinting) _currentSpeed = runSpeed;
        else
            _currentSpeed = walkSpeed;
    }
}
