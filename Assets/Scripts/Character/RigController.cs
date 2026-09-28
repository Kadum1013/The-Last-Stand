
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigController : MonoBehaviour
{
    [SerializeField] Rig aimRig;
    [SerializeField] Rig nonAimRig;
    [SerializeField] TwoBoneIKConstraint rightArmAimIK;
    [SerializeField] TwoBoneIKConstraint rightArmNonAimIK;

    [SerializeField] float time;
    bool setAimRightHandIK;
    bool isAiming = false;


    private void Update()
    {
        if (isAiming)
        {
            // If i want it to go slow.
            //aimRig.weight = Mathf.MoveTowards(aimRig.weight, 1, time * Time.deltaTime);
            //nonAimRig.weight = Mathf.MoveTowards(nonAimRig.weight, 0, 1);

            // quick change into the weight of the rig
            aimRig.weight = 1;
            nonAimRig.weight = 0;

        }
        else
        {
            //aimRig.weight = Mathf.MoveTowards(aimRig.weight, 0, 1);
            //nonAimRig.weight = Mathf.MoveTowards(nonAimRig.weight, 1, time * Time.deltaTime);
            aimRig.weight = 0;
            nonAimRig.weight = 1;
        }

        if (setAimRightHandIK)
        {
            rightArmAimIK.weight = 1;
        }
        else
        {
            rightArmAimIK.weight = 0;
        }
    }
    public void Aiming()
    {
        isAiming = true;
    }
    public void NotAiming()
    {
        isAiming = false;
    }

    public void Moving(bool isMoving)
    {
        if (isMoving)
        {
            rightArmNonAimIK.weight = 0;

        }
        else
        {
            rightArmNonAimIK.weight = 1;
        }
    }
    // Set right hand IK to 0
    public void DisableRightHandIK()
    {
        setAimRightHandIK = false;
    }
    // Set right hand IK to 1
    public void EnableRightHandIK()
    {
        setAimRightHandIK = true;
    }
}
