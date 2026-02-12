using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -19.62f;
    [SerializeField] private float groundCheckDist = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPivot;

    private float _verticalVelocity;
    public bool IsGrounded { get; private set; }
    public float VerticalVelocity => _verticalVelocity;

    private void Update()
    {
        Vector3 origin = feetPivot ? feetPivot.position : transform.position;
        IsGrounded = Physics.CheckSphere(origin, groundCheckDist, groundLayer);

        if (IsGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = -2f; // Small constant to stay stuck to ground
        }

        // Apply gravity
        _verticalVelocity += gravity * Time.deltaTime;
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            IsGrounded = false;
        }
    }
}
