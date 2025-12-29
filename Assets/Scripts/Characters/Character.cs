using UnityEngine;

[RequireComponent(typeof(MoveBehaviour))]
[RequireComponent(typeof(AnimationBehaviour))]
public class Character : MonoBehaviour
{
    protected MoveBehaviour _moveBehaviour;
    protected AnimationBehaviour _animationBehaviour;
}
