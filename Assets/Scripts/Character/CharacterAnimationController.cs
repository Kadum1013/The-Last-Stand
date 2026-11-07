
using System;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
    public Animator GetAnimator => animator;
     string shootArrowString = "shoot";
     string reloadArrowString = "reload";
     string aim = "isAiming";
     string moveXString = "moveX";
     string moveZString = "moveY";
     string drawSpeedString = "drawSpeed";

    

     string drawLayerName = "UpperBody";
     string drawStateName = "To Draw";   // the STATE name in the Animator, not the clip name
     string holdStateName = "";       // optional: state after the draw finishes (aim hold)

    string aimShakeString = "aimShack";
    string aimShakeSpeedString = "aimShackSpeed";

    // Death strings
    string headDeathString = "A_H_F_Death";
    string headRightDeathString = "A_H_R_Death";
    string headLeftDeathString = "A_H_L_Death";
    string spineBackDeathString = "A_S_B_Death";
    string stomachDeathString = "A_S_Death";



    //Hit direction strings
    string hitFrontString = "frontHit";
    string hitBackString = "backHit";
    string hitLeftString = "leftHit";
    string hitRightString = "rightHit";


    // Hit type of hit strings
    string hitHeadString = "headHit";
    string hitBodyString = "bodyHit";
    string hitShoulderString = "shoulderHit";

    static readonly int NoneHash = Animator.StringToHash("None");
    int drawLayer;
    float xValue;
    float yValue;

   [SerializeField]  bool aimIsShaking = false;
    float aimShakeSpeed = 0.2f;

    // Speed at which the animation will reach the direction value to play the right animation
    [SerializeField] float movementSpeedChanger = 3;
    public event Action OnGrabBowString;
    public event Action OnSpawnArrow;
    public event Action OnSetArrowRightHand;

    // Shot in progress
    int shotInProgress = 0;
    public void SetShotInProgress(int value) { shotInProgress = value; }
    public bool GetIsShotInProgress() { return shotInProgress == 1; }

    // For when the bow is loaded and not loaded.
    // 1 means true and 0 (or anyother number) means false.
    int bowIsLoaded = 0;
    public void SetBowIsLoaded(int value) { bowIsLoaded = value; }
    public bool GetIsBowLoaded => bowIsLoaded == 1;



    void Awake()
    {
        drawLayer = animator.GetLayerIndex(drawLayerName);
        if (drawLayer < 0) Debug.LogError($"Animator layer '{drawLayerName}' not found");

    }
    private void Update()
    {
        if (aimIsShaking)
        {
            aimShakeSpeed = Mathf.Clamp(aimShakeSpeed + 1 * Time.deltaTime, 0, 2);
            animator.SetFloat(aimShakeSpeedString, aimShakeSpeed += 1 * Time.deltaTime);
        }
        else
        {
            aimShakeSpeed = 0.2f;
            animator.SetFloat(aimShakeSpeedString, aimShakeSpeed);
        }
    }
    public void SwitchAnimatorController(AnimatorOverrideController overrideController)
    {
        animator.runtimeAnimatorController = overrideController;
    }

    public void SetAimShacking(bool isShacking)
    {
        aimIsShaking = isShacking;
        animator.SetBool(aimShakeString, isShacking);
    }

    // Check if character is aiming or not.
    public void SetIsAiming(bool isAiming)
    {
        animator.SetBool(aim, isAiming);
    }
    public void ResetLayersToNone()
    {
        for(int i = 1; i < animator.layerCount; i++)
        {
            if (animator.HasState(i, NoneHash))
                animator.CrossFadeInFixedTime(NoneHash, 0.1f, i);
        }
    }
    // Sets the draw speed Animation
    public void SetDrawSpeed(float value)
    {
        animator.SetFloat(drawSpeedString, value);
    }
    public void ShootArrow()
    {
        bowIsLoaded = 0;
        animator.SetTrigger(shootArrowString);
    }

    public void LoadArrow()
    {
        //animator.SetTrigger(reloadArrowString);
    }
    // Update character movement to the animator
    public void UpdateAnimationMovement(float moveXValue, float moveYValue)
    {
        xValue = Mathf.MoveTowards(xValue, moveXValue, movementSpeedChanger * Time.deltaTime);
        yValue = Mathf.MoveTowards(yValue, moveYValue, movementSpeedChanger * Time.deltaTime);
        animator.SetFloat(moveXString, xValue);
        animator.SetFloat(moveZString, yValue);
    }

    public float GetDrawProgress()
    {
        if (drawLayer < 0) return 0f;

        AnimatorStateInfo state = animator.IsInTransition(drawLayer)
            ? animator.GetNextAnimatorStateInfo(drawLayer)
            : animator.GetCurrentAnimatorStateInfo(drawLayer);

        if (!string.IsNullOrEmpty(holdStateName) && state.IsName(holdStateName)) return 1f;
        if (!state.IsName(drawStateName)) return 1f;

        return Mathf.Clamp01(state.normalizedTime);
    }

    public void Death(BodyPart bodyPart, HitImpactDirection hitImpactDirection)
    {
        animator.SetBool("death", true);
        animator.SetTrigger(GetDeathBodyPartAnimationString(bodyPart));
        // Check which party part it was
        // play the right animation.
    }
    public void ResetHitAnimation()
    {
        // Direction
        animator.SetBool(hitLeftString, false);
        animator.SetBool(hitRightString, false);
        animator.SetBool(hitFrontString, false);
        animator.SetBool(hitBackString, false);

        // Body part
        animator.SetBool(hitHeadString, false);
        animator.SetBool(hitBodyString, false);
        animator.SetBool(hitShoulderString, false);

    }
    public void HitAnimation(BodyPart bodyPart, HitImpactDirection hitImpactDirection)
    {
        // Get animation string for the right body part, to set trigger.
        string bodyPartString = GetBodyPartAnimationString(bodyPart);
        // Get animation string for the direction of impact, to set the bool.  
        string hitDirectionString = GetHitDirectionAnimationString(hitImpactDirection);
        if (bodyPart == BodyPart.LeftArm || bodyPart == BodyPart.LeftLeg)
        {
             hitDirectionString = hitLeftString;
        }
        else if(bodyPart == BodyPart.RightArm || bodyPart == BodyPart.RightLeg)
        {
            hitDirectionString = hitRightString;
        }

        animator.SetBool(hitDirectionString, true);
        animator.SetBool(bodyPartString, true);
    }

    // Death body part animation strings
    private string GetDeathBodyPartAnimationString(BodyPart bodyPart)
    {
        string bodyPartString;

        switch (bodyPart)
        {
            case BodyPart.Head: bodyPartString = headDeathString; break;
            case BodyPart.Body: bodyPartString = stomachDeathString; break;
            case BodyPart.LeftArm: bodyPartString = headLeftDeathString; break;
            case BodyPart.RightArm: bodyPartString = headRightDeathString; break;
            case BodyPart.LeftLeg: bodyPartString = stomachDeathString; break;
            case BodyPart.RightLeg: bodyPartString = stomachDeathString; break;
            default: bodyPartString = hitBodyString; break;
        }

        return bodyPartString;
    }


    // Hit body part animation strings
    private string GetBodyPartAnimationString(BodyPart bodyPart)
    {
        string bodyPartString;

        switch (bodyPart)
        {
            case BodyPart.Head: bodyPartString = hitHeadString; break;
            case BodyPart.Body: bodyPartString = hitBodyString; break;
            case BodyPart.LeftArm: bodyPartString = hitShoulderString; break;
            case BodyPart.RightArm: bodyPartString = hitShoulderString; break;
            case BodyPart.LeftLeg: bodyPartString = hitShoulderString; break;
            case BodyPart.RightLeg: bodyPartString = hitShoulderString; break;
            default: bodyPartString = hitBodyString; break;
        }

        return bodyPartString;
    }
    private string GetHitDirectionAnimationString(HitImpactDirection hitDirection)
    {
        string direction;
        switch (hitDirection)
        {
            case HitImpactDirection.forwardImpact: direction = hitFrontString; break;
            case HitImpactDirection.backImpact: direction = hitBackString; break;
            case HitImpactDirection.leftImpact: direction = hitLeftString; break;
            case HitImpactDirection.rightImpact: direction = hitRightString; break;
            default: direction = hitFrontString; break;
        }

        return direction;
    }

    // Event for spawn arrow
    public void InvokeOnSpawnArrow() => OnSpawnArrow?.Invoke();
    public void InvokeOnSetArrowRightHand() => OnSetArrowRightHand?.Invoke();
    public void InvokeOnGrabBowString() { OnGrabBowString?.Invoke(); }

}
