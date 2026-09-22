
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    [Tooltip(" The LayerMask Ray cast will hit.")]
    [SerializeField] LayerMask layerMask;

    [Tooltip("The Transform the camera and player will look at")]
    [SerializeField] Transform targetLookAtTransform;

    [Tooltip("Main camera")]
    [SerializeField] Camera mainCamera;

    [Tooltip("Spped at which you rotate")]
    [SerializeField] float turnSpeed = 15f;

    [Tooltip("If player is aiming or not")]
    [SerializeField] bool isAiming;
    
    // Set the character to aiming.
    public void SetIsAiming(bool aiming)
    {
        isAiming = aiming;
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
            Quaternion targetRot = Quaternion.LookRotation(camForward);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);
        }
    }
    private void LookAtTarget()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = mainCamera.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, layerMask))
        {
            targetLookAtTransform.position = hitInfo.point;
            mouseWorldPosition = hitInfo.point;
        }

        if (isAiming)
        {
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDir = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 20f);
        }
    }


}
