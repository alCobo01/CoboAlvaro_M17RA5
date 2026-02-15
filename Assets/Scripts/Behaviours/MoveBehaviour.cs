using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoveBehaviour : MonoBehaviour
{
    public CharacterController Controller { get; set; }
    public float Speed { get; private set; }
    
    private Vector3 _velocity;
    
    private void Awake() => Controller = GetComponent<CharacterController>();

    public void Move(Vector3 direction, float speed)
    {
        var move = direction.normalized * (speed * Time.deltaTime);
        Controller.Move(move);
        Speed = new Vector3(Controller.velocity.x, 0, Controller.velocity.z).magnitude;
    }

    public void ApplyGravity(float verticalVelocity)
    {
        _velocity.y = verticalVelocity;
        Controller.Move(_velocity * Time.deltaTime);
    }
}
