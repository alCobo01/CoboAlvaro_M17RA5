using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerCameraController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerInputController inputController;
    [SerializeField] private Transform cameraPivot;
    
    [Header("Camera Settings")]
    [SerializeField] private float sensitivityX = 0.5f;
    [SerializeField] private float sensitivityY = 0.5f;
    [SerializeField] private float minPitch = -40f;
    [SerializeField] private float maxPitch = 60f;

    public bool canRotate = true;
    private float _pitch, _yaw;

    private void Awake()
    {
        if (!inputController) inputController = GetComponent<PlayerInputController>();
        inputController.OnLookEvent += HandleLook;
        
        var currentRotation = cameraPivot.eulerAngles;
        _yaw = currentRotation.y;
        _pitch = currentRotation.x;
        if (_pitch > 180) _pitch -= 360;
    }

    private void OnDestroy()
    {
        if (inputController) inputController.OnLookEvent -= HandleLook;
    }

    private void HandleLook(Vector2 input)
    {
        if (!canRotate) return;
        
        _yaw += input.x * sensitivityX;
        _pitch -= input.y * sensitivityY;

        _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);
    }

    private void LateUpdate() => RotateAndMoveCamera();

    private void RotateAndMoveCamera()
    {
        var lookRotation = Quaternion.Euler(_pitch, _yaw, 0f);
        cameraPivot.rotation = lookRotation;
    }
}
