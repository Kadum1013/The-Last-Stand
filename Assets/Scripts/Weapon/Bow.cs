using System.Collections;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] Transform spawnArrowPos;

    // Bow string

    [SerializeField] Transform tip01;
    [SerializeField] Transform tip02;

    [SerializeField] Transform limb01;
    [SerializeField] Transform limb02;

    [SerializeField] Transform bowString;
    [SerializeField] Transform bowstringAnchorPoint;
    [SerializeField] Transform arrowRestPoint;

    Transform rightHandIKPoint;


    [SerializeField] float baseBowDamage = 4;


    [Header("Limb Bending")]
    [SerializeField] float bendAmount = 15f;          // degrees at full pull
    [SerializeField] float maxPullDistance = 0.4f;    // string distance (meters) that counts as a full pull
    [SerializeField] float bendSmoothing = 20f;       // higher = snappier

    private float currentBend01;

   [SerializeField] Transform TestLookAt;

    // Rest point for the string when not aiming.
    [SerializeField] Transform bowStringRestPoint;
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
        // how far the string is pulled back from its rest position
        float pull = Vector3.Distance(bowString.position, bowStringRestPoint.position);
        float target01 = Mathf.Clamp01(pull / maxPullDistance);

        // smooth it so the limbs ease in and spring back instead of snapping
        currentBend01 = Mathf.Lerp(currentBend01, target01, 1f - Mathf.Exp(-bendSmoothing * Time.deltaTime));

        float bend = bendAmount * currentBend01;

        limb01.localEulerAngles = new Vector3(
        initialLimb01LocalEulerAngles.x,
        initialLimb01LocalEulerAngles.y - bend,
        initialLimb01LocalEulerAngles.z);

        limb02.localEulerAngles = new Vector3(
            initialLimb02LocalEulerAngles.x,
            initialLimb02LocalEulerAngles.y - bend,
            initialLimb02LocalEulerAngles.z);

        if (currentArrow != null)
        {
            if (isNocked)
            {
                currentArrow.transform.position = bowString.position;
                currentArrow.transform.rotation = arrowRestPoint.rotation;
            }
            else
            {
                currentArrow.transform.position = spawnArrowPos.position;
                currentArrow.transform.rotation = spawnArrowPos.rotation;
            }
        }

    }


    public void SetRightHandIK(Transform rightHandIKPoint)
    {
        this.rightHandIKPoint = rightHandIKPoint;
    }

    public void LoadArrow(Arrow arrow)
    {
        isNocked = true;
        currentArrow = null;
        currentArrow = arrow;

        // Creates arrow and attaches it to the bow
        currentArrow.transform.SetParent(spawnArrowPos.transform);
        currentArrow.transform.position = spawnArrowPos.transform.position;
        currentArrow.transform.rotation = spawnArrowPos.transform.rotation;
    }

    public void SetHoldingBowString(bool holding)
    {
        if (holding)
        {
            bowString.SetParent(rightHandIKPoint);
            bowString.position = rightHandIKPoint.position;
        }
        else
        {
            bowString.SetParent(bowStringRestPoint);
            bowString.position = bowStringRestPoint.position;
        }
    }
    public void ShootArrow(float maxPowerDraw, Transform lookAt, float drawPercent)
    {
        // If there isnt an arrow ready to shoot, skip
        if (currentArrow != null)
        {

            float launchPower = maxPowerDraw * drawPercent;
            float damage = baseBowDamage * drawPercent;

            // Detache it from the bow gameobject.
            currentArrow.transform.SetParent(null);

            // Let go of the string
            SetHoldingBowString(false);

            // The arrow is no longer on nocked (on the bow string)
            isNocked = false;

            // shoot the arrow before removing it from cache
            currentArrow.ShootArrow(damage, launchPower, lookAt);
            currentArrow = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(spawnArrowPos.position, TestLookAt.position);
    }
}
