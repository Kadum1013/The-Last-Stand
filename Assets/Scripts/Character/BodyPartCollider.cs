using UnityEngine;

public class BodyPartCollider : MonoBehaviour
{
    [SerializeField] BodyPart bodyPart;

    [SerializeField] HitDetector hitDetector;

    [SerializeField] FacingAxis frontFacingAxis;
    [SerializeField] FacingAxis sideFacingAxis;
    [SerializeField] float damageMultiplier;
    


    private void Awake()
    {
        if (!hitDetector)
            hitDetector = GetComponentInParent<HitDetector>();
    }

    public void Hit(int incomingDmg, Vector3 directionOfImpact)
    {
        Vector3 frontFacingDirection = GetBodyPartFacingDirection(frontFacingAxis);
        Vector3 sideFacingDirection = GetBodyPartFacingDirection(sideFacingAxis);

        float forwardCheck = Vector3.Dot(frontFacingDirection, directionOfImpact);
        float sideCheck = Vector3.Dot(sideFacingDirection, directionOfImpact);

        HitImpactDirection hitImpactDirection = HitImpactDirection.forwardImpact;
        if(forwardCheck < -0.5f) { hitImpactDirection = HitImpactDirection.forwardImpact; Debug.Log("Front!"); }
        else if (forwardCheck > 0.5f) { hitImpactDirection = HitImpactDirection.backImpact; Debug.Log("Back!"); }
        else if (sideCheck < -0.5f) { hitImpactDirection = HitImpactDirection.leftImpact; Debug.Log("Left!"); }
        else if (sideCheck > 0.5f) { hitImpactDirection = HitImpactDirection.rightImpact; Debug.Log("Right!"); }
        else { Debug.Log("Is angled between sides!"); }


        // Pass along the direction.
        // Check if it hit the frontOrBack, if yes set a bool
        // then set if its forward or back.
        float damageMultiplied = incomingDmg * damageMultiplier;
        int damaged = Mathf.RoundToInt(damageMultiplied);

        hitDetector.Hit(damaged, bodyPart, hitImpactDirection);
    }

    private Vector3 GetBodyPartFacingDirection(FacingAxis direction)
    {
        switch (direction)
        {
            case FacingAxis.PositiveX: return transform.right;
            case FacingAxis.NegativeX: return -transform.right;
            case FacingAxis.PositiveY: return transform.up;
            case FacingAxis.NegativeY: return -transform.up;
            case FacingAxis.PositiveZ: return transform.forward;
            case FacingAxis.NegativeZ: return -transform.forward;
            default: return transform.forward;
        }
    }
}