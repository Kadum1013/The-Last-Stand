using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera aimVirtualCamera;
    [SerializeField] float normalSensitivity;
    [SerializeField] float aimSensitivity;

    // Sensitivity is set from thridpersoncontroller, when aiming and not aiming.
    private float sensitivity = 1f;

    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    [Header("Player")]
    [Tooltip("Input system")]
    [SerializeField] PlayerInput playerInput;


    [Header("Camera")]
    [Tooltip("Main camera")]
    [SerializeField] Camera mainCamera;
    [Tooltip("The Transform the Camera will look at and follow.")]
    [SerializeField] GameObject CinemachineCameraTarget;

    [Tooltip("If the player is using a keyboard and mouse")]
    [SerializeField] bool IsCurrentDeviceMouse;
    [Tooltip("Lock cameras rotation")]
    [SerializeField] public bool LockCameraPosition = false;
    [Tooltip("Override the cameras angle position")]
    [SerializeField] float CameraAngleOverride = 0.0f;
    [Tooltip("Bottom cameras clamp")]
    [SerializeField] float BottomClamp = -30.0f;
    [Tooltip("Top cameras clamp")]
    [SerializeField] float TopClamp = 70.0f;

    private const float _threshold = 0.01f;
    private void Start()
    {
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
    }

    private void LateUpdate()
    {
        CameraRotation();
    }
    private void CameraRotation()
    {
        // if there is an input and camera position is not fixed
        if (playerInput.MousePosition.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            //Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cinemachineTargetYaw += playerInput.MousePosition.x * deltaTimeMultiplier * sensitivity;
            _cinemachineTargetPitch -= playerInput.MousePosition.y * deltaTimeMultiplier * sensitivity;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    public void SetAiming(bool isAiming)
    {
        if (isAiming)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            sensitivity = aimSensitivity;
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            sensitivity = normalSensitivity;
        }
    }
}
