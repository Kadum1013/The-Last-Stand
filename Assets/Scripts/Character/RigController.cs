
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigController : MonoBehaviour
{
    [SerializeField] Rig aimRig;
    [SerializeField] Rig nonAimRig;
    [SerializeField] TwoBoneIKConstraint rightArmAimIK;
    [SerializeField] TwoBoneIKConstraint rightArmNonAimIK;

    [SerializeField] float time;
    bool isAiming = false;
    private void Update()
    {
        if (isAiming)
        {
            aimRig.weight = Mathf.MoveTowards(aimRig.weight, 1, time * Time.deltaTime);
            nonAimRig.weight = Mathf.MoveTowards(nonAimRig.weight, 0, 1);
        }
        else
        { 
            aimRig.weight = Mathf.MoveTowards(aimRig.weight, 0, 1);
            nonAimRig.weight = Mathf.MoveTowards(nonAimRig.weight, 1, time * Time.deltaTime);
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
    public void LoadBow()
    {
        StartCoroutine(LoadingBow());
    }

    private IEnumerator LoadingBow()
    {
        yield return new WaitForSeconds(0.1f);
        rightArmAimIK.weight = 0;
        yield return new WaitForSeconds(0.5f);
        rightArmAimIK.weight = 1;
    }

}
