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
        var origin = feetPivot ? feetPivot.position : transform.position;
        IsGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDist);
        
        if (IsGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = -2f;
        }
        
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
