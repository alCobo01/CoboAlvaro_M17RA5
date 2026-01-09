using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(PlayerAttackController))]
[RequireComponent(typeof(AnimationBehaviour))]
public class CombatModeManager : MonoBehaviour
{
    private PlayerInputController _inputController;
    private PlayerAttackController _attackController;
    private AnimationBehaviour _animationBehaviour;
    
    // Dependencies on specific strategies
    [SerializeField] private MeleeAttack _meleeAttack;
    [SerializeField] private RangeAttack _rangeAttack;

    private void Awake()
    {
        _inputController = GetComponent<PlayerInputController>();
        _attackController = GetComponent<PlayerAttackController>();
        _animationBehaviour = GetComponent<AnimationBehaviour>();
        
        // Auto-detect if not assigned (assumes they are on the same GameObject)
        if (_meleeAttack == null) _meleeAttack = GetComponent<MeleeAttack>();
        if (_rangeAttack == null) _rangeAttack = GetComponent<RangeAttack>();
    }

    private void OnEnable()
    {
        _inputController.OnAimEvent += HandleAim;
        
        // Initialize with default strategy (Melee)
        // We do this in Start or OnEnable to ensure everything is initialized
        if (_meleeAttack != null)
        {
            _attackController.SetAttackStrategy(_meleeAttack);
        }
    }

    private void OnDisable()
    {
        _inputController.OnAimEvent -= HandleAim;
    }

    private void HandleAim(bool isAiming)
    {
        _animationBehaviour.SetAiming(isAiming);

        if (isAiming && _rangeAttack != null)
        {
            _attackController.SetAttackStrategy(_rangeAttack);
        }
        else if (!isAiming && _meleeAttack != null)
        {
            _attackController.SetAttackStrategy(_meleeAttack);
        }
    }
}
