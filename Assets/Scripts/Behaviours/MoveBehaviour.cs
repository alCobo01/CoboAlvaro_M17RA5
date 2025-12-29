using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveBehaviour : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    public void Move(Vector3 direction, float speed)
    {
        var targetVelocity = direction.normalized * speed;
        targetVelocity.y = _rigidbody.linearVelocity.y;

        _rigidbody.linearVelocity = targetVelocity;
    }
}
