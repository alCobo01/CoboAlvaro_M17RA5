using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private void LateUpdate()
    {
        transform.position = new Vector3(playerTransform.position.x, 40f, playerTransform.position.z);
    }
}
