using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class AnimationBehaviour : MonoBehaviour
{
    private static readonly int _speedHash = Animator.StringToHash("Speed");
    private static readonly int _danceHash = Animator.StringToHash("Dance");
    private static readonly int _jumpHash = Animator.StringToHash("Jump");
    private static readonly int _isGroundedHash = Animator.StringToHash("IsGrounded");
    private static readonly int _verticalSpeedHash = Animator.StringToHash("VerticalSpeed");
    private static readonly int _isCrouchedHash = Animator.StringToHash("IsCrouched");

    private Animator _animator;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 velocity = _rigidbody.linearVelocity;
        Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);
        float currentSpeed = horizontalVelocity.magnitude;
        _animator.SetFloat(_speedHash, currentSpeed, 0.1f, Time.deltaTime);
    }

    public void PlayDance()
    {
        Debug.Log("Dance triggered");
        _animator.SetTrigger(_danceHash);
    }

    public void TriggerJump()
    {
        _animator.SetTrigger(_jumpHash);
    }

    public void SetGrounded(bool isGrounded)
    {
        _animator.SetBool(_isGroundedHash, isGrounded);
    }

    public void SetVerticalVelocity(float velocity)
    {
        _animator.SetFloat(_verticalSpeedHash, velocity);
    }

    public void SetCrouch(bool isCrouching)
    {
        _animator.SetBool(_isCrouchedHash, isCrouching);
    }
} 
