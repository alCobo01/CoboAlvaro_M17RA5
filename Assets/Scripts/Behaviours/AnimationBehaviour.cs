using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class AnimationBehaviour : MonoBehaviour
{
    private static readonly int _speedHash = Animator.StringToHash("Speed");
    private static readonly int _danceHash = Animator.StringToHash("Dance");

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
        _animator.SetTrigger(_danceHash);
    }
} 
