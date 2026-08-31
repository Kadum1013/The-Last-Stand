using UnityEngine;


public class AimTargetController : MonoBehaviour
{
    //[SerializeField] Camera mainCamera;
    //[SerializeField] LayerMask layerMask;
    //[SerializeField] Transform debugTransform;
    //[SerializeField] Transform aimRotation;


    //private void Update()
    //{
    //    Vector3 mouseWorldPosition = Vector3.zero;
    //    Vector2 screenCenterPoint = new Vector2 (Screen.width / 2f, Screen.height / 2f);
    //    Ray ray = mainCamera.ScreenPointToRay(screenCenterPoint);
    //    if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, layerMask))
    //    {
    //        debugTransform.position = hitInfo.point;
    //        mouseWorldPosition = hitInfo.point;
    //    }

    //    Vector3 worldAimTarget = mouseWorldPosition;
    //    worldAimTarget.y = transform.position.y;
    //    Vector3 aimDir = (worldAimTarget - transform.position).normalized;

    //    aimRotation.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 20f);
    //}

}
