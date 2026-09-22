using System.Collections;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] Transform spawnArrowPos;

    // Bow string
    [SerializeField] LineRenderer bowstringLine;

    [SerializeField] Transform tip01;
    [SerializeField] Transform tip02;

    [SerializeField] Transform limb01;
    [SerializeField] Transform limb02;

    [SerializeField] Transform nockPoint;
    [SerializeField] Transform bowstringAnchorPoint;

    [SerializeField] float delay;
    [SerializeField] float duration;

    // Rest point for the string when not aiming.
    [SerializeField] Transform nockPointRestPoint;
    private Vector3 initialLimb01LocalEulerAngles;
    private Vector3 initialLimb02LocalEulerAngles;

    [SerializeField] Quaternion arrowRestRotationOffset;
    bool isNocked = false;

    // Cache to hold current Arrow that is loaded.
    Arrow currentArrow;

    private void OnEnable()
    {
        if (limb01 && limb02)
        {
            initialLimb01LocalEulerAngles = limb01.localEulerAngles;
            initialLimb02LocalEulerAngles = limb02.localEulerAngles;
        }
    }

    private void LateUpdate()
    {
        CreateBowstring();

        if (currentArrow != null)
        {
            if (isNocked)
            {
                currentArrow.transform.position = nockPoint.position;
                currentArrow.transform.rotation = this.transform.rotation * arrowRestRotationOffset;
            }
            else
            {
                currentArrow.transform.position = spawnArrowPos.position;
                currentArrow.transform.rotation = spawnArrowPos.rotation;
            }
        }

    }

    void CreateBowstring()
    {
        if (!bowstringLine || !tip01 || !tip02 || !nockPoint)
        {
             return;
        }

        bowstringLine.positionCount = 3;
        bowstringLine.SetPosition(0, tip01.position);
        bowstringLine.SetPosition(1, nockPoint.position);
        bowstringLine.SetPosition(2, tip02.position);
    }
#if UNITY_EDITOR
    //Places bowstring even in Edit mode
    void OnValidate()
    {
        CreateBowstring();
    }
#endif 


    public void LoadArrow(Arrow arrow, Transform rightHandIKPoint)
    {
        isNocked = true;
        currentArrow = null;
        currentArrow = arrow;
        // Creates arrow and attaches it to the bow
        currentArrow.transform.parent = spawnArrowPos.transform;
        currentArrow.transform.position = spawnArrowPos.transform.position;
        currentArrow.transform.rotation = spawnArrowPos.transform.rotation;

        nockPoint.parent = rightHandIKPoint;
        nockPoint.position = rightHandIKPoint.position;
    }
    public void ShootArrow()
    {
        // If there isnt an arrow ready to shoot, skip
        if (currentArrow != null)
        {
            // Detache it from the bow gameobject.
            currentArrow.transform.SetParent(null);
            nockPoint.parent = nockPointRestPoint;
            nockPoint.position = nockPointRestPoint.position;
            isNocked = false;

            currentArrow.ShootArrow();
            currentArrow = null;
        }
    }
}
