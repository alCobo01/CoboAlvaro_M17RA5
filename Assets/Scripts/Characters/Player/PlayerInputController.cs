using static InputSystem_Actions;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine;

public class PlayerInputController : MonoBehaviour, IPlayerActions
{
    public event UnityAction<Vector2> OnLookEvent = delegate { };
    public event UnityAction<Vector2> OnMoveEvent = delegate { };
    public event UnityAction<bool> OnSprintEvent = delegate { };
    public event UnityAction<bool> OnCrouchEvent = delegate { };
    public event UnityAction<bool> OnAimEvent = delegate { };
    public event UnityAction OnJumpEvent = delegate { };
    public event UnityAction OnDanceEvent = delegate { };
    public event UnityAction OnAttackEvent = delegate { };
    public event UnityAction OnInteractEvent = delegate { };

    private InputSystem_Actions _inputActions;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.SetCallbacks(this);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable() => _inputActions.Enable();
    private void OnDisable() => _inputActions.Disable();

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        OnLookEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed) OnSprintEvent.Invoke(true);
        if (context.canceled) OnSprintEvent.Invoke(false);
    }

    public void OnDance(InputAction.CallbackContext context)
    {
        if (context.performed) OnDanceEvent.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context) 
    {
        if (context.performed) OnAttackEvent.Invoke();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed) OnAimEvent.Invoke(true);
        if (context.canceled) OnAimEvent.Invoke(false);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) OnJumpEvent.Invoke();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed) OnCrouchEvent.Invoke(true);
        if (context.canceled) OnCrouchEvent.Invoke(false);
    }
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed) OnInteractEvent.Invoke();   
    }
}
