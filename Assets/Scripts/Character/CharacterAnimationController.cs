
using System;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] string shootArrowString = "shoot";
    [SerializeField] string reloadArrowString = "reload";
    [SerializeField] string aim = "isAiming";
    [SerializeField] string moveXString = "moveX";
    [SerializeField] string moveZString = "moveY";
    [SerializeField] string drawSpeedString = "drawSpeed";

    // Speed at which the animation will reach the direction value to play the right animation
    [SerializeField] float movementSpeedChanger = 3;

    [SerializeField] string drawLayerName = "UpperBody";
    [SerializeField] string drawStateName = "Draw";   // the STATE name in the Animator, not the clip name
    [SerializeField] string holdStateName = "";       // optional: state after the draw finishes (aim hold)

    int drawLayer;
    float xValue;
    float yValue;

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

    // Check if character is aiming or not.
    public void SetIsAiming(bool isAiming)
    {
        animator.SetBool(aim, isAiming);
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
        animator.SetTrigger(reloadArrowString);
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

    // Event for spawn arrow
    public void InvokeOnSpawnArrow() => OnSpawnArrow?.Invoke();
    public void InvokeOnSetArrowRightHand() => OnSetArrowRightHand?.Invoke();
    public void InvokeOnGrabBowString() { OnGrabBowString?.Invoke(); }
}
