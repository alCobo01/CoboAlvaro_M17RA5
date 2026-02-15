using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(PlayerAttackController))]
[RequireComponent(typeof(AnimationBehaviour))]
public class CombatModeManager : MonoBehaviour
{
    private PlayerInputController _inputController;
    private PlayerAttackController _attackController;
    private PlayerCameraController _cameraController;
    private AnimationBehaviour _animationBehaviour;
    
    // Dependencies on specific strategies
    [SerializeField] private MeleeAttack meleeAttack;
    [SerializeField] private RangeAttack rangeAttack;

    private void Awake()
    {
        _inputController = GetComponent<PlayerInputController>();
        _attackController = GetComponent<PlayerAttackController>();
        _cameraController = GetComponent<PlayerCameraController>();
        _animationBehaviour = GetComponent<AnimationBehaviour>();
        
        if (meleeAttack == null) meleeAttack = GetComponent<MeleeAttack>();
        if (rangeAttack == null) rangeAttack = GetComponent<RangeAttack>();
    }

    private void OnEnable()
    {
        _inputController.OnAimEvent += HandleAim;
        
        if (meleeAttack != null)
            _attackController.SetAttackStrategy(meleeAttack);
    }
    
    private void OnDisable()
    {
        _inputController.OnAimEvent -= HandleAim;
    }

    private void HandleAim(bool isAiming)
    {
        _animationBehaviour.SetAiming(isAiming);
        switch (isAiming)
        {
            case true when rangeAttack != null:
                _attackController.SetAttackStrategy(rangeAttack);
                _cameraController.canRotate = false;
                break;
            case false when meleeAttack != null:
                _attackController.SetAttackStrategy(meleeAttack);
                _cameraController.canRotate = true;
                break;
        }
    }
}
