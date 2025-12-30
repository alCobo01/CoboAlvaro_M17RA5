using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerCameraController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerInputController inputController;
    [SerializeField] private Transform cameraPivot;

    [Header("Camera settings")]
    [SerializeField] private float lookSensitivity = 0.5f;
    [SerializeField] private float smoothInputSpeed = 0.05f;
    [SerializeField] private float minPitch = -10f;
    [SerializeField] private float maxPitch = 10f;

    private float _pitch, _yaw;
    private Vector2 _currentLookInput;
    private Vector2 _targetLookInput;
    private Vector2 _smoothVelocity;

    private void Awake() => inputController.OnLookEvent += HandleLook;

    private void HandleLook(Vector2 input) => _targetLookInput = input;

    private void LateUpdate() => RotateCamera();

    private void RotateCamera()
    {
        _currentLookInput = Vector2.SmoothDamp(_currentLookInput, _targetLookInput, ref _smoothVelocity, smoothInputSpeed);

        if (_currentLookInput.sqrMagnitude < 0.00001f) return;

        // Horizontal Rotation (Yaw)
        _yaw += _currentLookInput.x * lookSensitivity;

        // Vertical Rotation (Pitch)
        _pitch -= _currentLookInput.y * lookSensitivity;
        _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);

        cameraPivot.rotation = Quaternion.Euler(_pitch, _yaw, 0f);

    }
}
