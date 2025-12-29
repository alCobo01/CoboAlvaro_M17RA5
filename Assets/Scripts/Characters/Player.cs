using static InputSystem_Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character, IPlayerActions
{
    [SerializeField] private float speed = 5f;

    private InputSystem_Actions _inputActions;
    private Vector3 _moveInput;

    private void Awake()
    {
        _moveBehaviour = GetComponent<MoveBehaviour>();
        _animationBehaviour = GetComponent<AnimationBehaviour>();

        _inputActions = new InputSystem_Actions();
        _inputActions.Player.SetCallbacks(this);
    }

    void Start() => _inputActions.Enable();
    void OnEnable() => _inputActions.Enable();
    void OnDisable() => _inputActions.Disable();

    private void FixedUpdate()
    {
        _moveBehaviour.Move(_moveInput, speed);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        
    }

    public void OnDance(InputAction.CallbackContext context) => _animationBehaviour.PlayDance();

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
        
    }
}
