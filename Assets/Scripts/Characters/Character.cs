using UnityEngine;

[RequireComponent(typeof(MoveBehaviour))]
[RequireComponent(typeof(AnimationBehaviour))]
public class Character : MonoBehaviour
{
    protected MoveBehaviour moveBehaviour;
    protected AnimationBehaviour animationBehaviour;
}
