using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class AnimationBehaviour : MonoBehaviour
{
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int DanceHash = Animator.StringToHash("Dance");
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
    private static readonly int IsCrouchedHash = Animator.StringToHash("IsCrouched");
    private static readonly int MeleeAttackHash = Animator.StringToHash("MeleeAttack");
    private static readonly int RangeAttackHash = Animator.StringToHash("RangeAttack");
    private static readonly int IsAimingHash = Animator.StringToHash("IsAiming");
    private static readonly int TalkHash = Animator.StringToHash("Talk");

    private Animator _animator;
    private MoveBehaviour _moveBehaviour;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _moveBehaviour = GetComponent<MoveBehaviour>();
    }

    private void Update()
    {
        _animator.SetFloat(SpeedHash, _moveBehaviour.Speed, 0.1f, Time.deltaTime);
    }

    public void PlayDance()
    {
        _animator.SetTrigger(DanceHash);
    }

    public void TriggerMeleeAttack()
    {
        _animator.SetTrigger(MeleeAttackHash);
    }

    public void TriggerRangeAttack()
    {
        _animator.SetTrigger(RangeAttackHash);
    }

    public void SetAiming(bool isAiming)
    {
        _animator.SetBool(IsAimingHash, isAiming);
    }

    public void TriggerJump()
    {
        _animator.SetTrigger(JumpHash);
    }

    public void TriggerTalk()
    {
        _animator.SetTrigger(TalkHash);
    }

    public void SetGrounded(bool isGrounded)
    {
        _animator.SetBool(IsGroundedHash, isGrounded);
    }

    public void SetCrouch(bool isCrouching)
    {
        _animator.SetBool(IsCrouchedHash, isCrouching);
    }
} 
