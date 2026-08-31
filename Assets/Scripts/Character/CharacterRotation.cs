
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;


    
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform debugTransform;
    [SerializeField] Animator animator;
    [SerializeField] PlayerAttack playerAttack;

    [Header("Player")]
    [Tooltip("Input system")]
    [SerializeField] PlayerInput playerInput;

    // Sensitivity is set from thridpersoncontroller, when aiming and not aiming.
    private float sensitivity = 1f;


    [Header("Camera")]
    [Tooltip("Main camera")]
    [SerializeField] Camera mainCamera;
    [Tooltip("The Transform the Camera will look at and follow.")]
    [SerializeField] GameObject CinemachineCameraTarget;
    [Tooltip("Spped at which you rotate")]
    [SerializeField] float turnSpeed = 15f;
    [Tooltip("Stance offset while aiming")]
    [SerializeField] float aimSideAngle = 90f; // stance offset while aiming
    [Tooltip("If player is aiming or not")]
    [SerializeField] bool isAiming;
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

    void Update()
    {
        FaceCameraForward();
        LookAtTarget();
    }

    // If character is moving, face the direction of the cameras forward axis
    private void FaceCameraForward()
    {
        Vector3 camForward = mainCamera.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        if (camForward.sqrMagnitude > 0.001f)
        {
            float sideOffset = isAiming ? aimSideAngle : 0f;
            Quaternion targetRot = Quaternion.LookRotation(camForward) * Quaternion.Euler(0f, sideOffset, 0f);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);
        }
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
    private void LookAtTarget()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = mainCamera.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, layerMask))
        {
            debugTransform.position = hitInfo.point;
            mouseWorldPosition = hitInfo.point;
        }

        if (isAiming)
        {
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDir = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 20f);
            playerAttack.SetLookAtDirection(mouseWorldPosition.normalized);
        }
    }

    public void SetIsAiming(float newSensitivity, bool aiming)
    {
        animator.SetBool("isAiming", aiming);
        isAiming = aiming;
        sensitivity = newSensitivity;
    }
}
