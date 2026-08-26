using UnityEngine;

public class AimTargetController : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform aimTarget;
    [SerializeField] float aimDistance = 10;

    private void LateUpdate()
    {
        aimTarget.position = cameraTransform.position + cameraTransform.forward * aimDistance;
    }
}
