using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpBehaviour : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundCheckDist = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPivot;

    private Rigidbody _rb;
    public bool IsGrounded { get; private set; }
    public float VerticalVelocity => _rb.linearVelocity.y;

    private void Awake() => _rb = GetComponent<Rigidbody>();

    private void FixedUpdate()
    {
        Vector3 origin = feetPivot ? feetPivot.position : transform.position;
        IsGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDist, groundLayer);
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            IsGrounded = false;
        }
    }
}
