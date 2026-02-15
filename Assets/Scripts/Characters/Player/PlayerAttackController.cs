using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(IAttack))]
public class PlayerAttackController : MonoBehaviour
{
        private PlayerInputController _inputController;
        private IAttack _currentAttack;
        
        private void Awake()
        {
            _inputController = GetComponent<PlayerInputController>();
            _currentAttack = GetComponent<IAttack>();

            // Subscribe to input events
            _inputController.OnAttackEvent += HandleAttack;
        }

         private void HandleAttack() => _currentAttack?.Attack();

         public void SetAttackStrategy(IAttack newAttackStrategy)
        {
            _currentAttack = newAttackStrategy;
        }
}