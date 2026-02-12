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
    private Vector2 _rawInput;
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

        // if (SaveManager.Instance.TryGetLoadedPlayerPosition(out var position))
        // {
        //     moveBehaviour.Controller.enabled = false;
        //     transform.position = position;
        //     moveBehaviour.Controller.enabled = true;
        // }
    }

    private void Update() 
    {
        animationBehaviour.SetGrounded(_jumpBehaviour.IsGrounded);
        CalculateMovement();
        RotatePlayer();
        moveBehaviour.Move(_moveInput, _currentSpeed);
        moveBehaviour.ApplyGravity(_jumpBehaviour.VerticalVelocity);
    }

    private void RotatePlayer()
    {
        if (_moveInput.sqrMagnitude > 0.001f)
        {
            var targetRotation = Quaternion.LookRotation(_moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void CalculateMovement()
    {
        if (_rawInput.sqrMagnitude < 0.01f)
        {
            _moveInput = Vector3.zero;
            return;
        }

        var camForward = cameraTransform.forward;
        var camRight = cameraTransform.right;

        // Flatten Y (to not walk into the ground/sky)
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();
        
        // Relative movement based on camera orientation
        _moveInput = (camForward * _rawInput.y) + (camRight * _rawInput.x);
    }

    private void HandleMove(Vector2 input) => _rawInput = input;
    

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
