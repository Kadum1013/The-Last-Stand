using System;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] string shootArrow = "shoot";
    [SerializeField] string reloadArrow = "reload";
    [SerializeField] string aim = "isAiming";
    [SerializeField] string aimMoving = "aimMoving";
    [SerializeField] string moveX = "moveX";
    [SerializeField] string moveZ = "moveY";

    [SerializeField] float speedAnimation = 3;
    float xValue;
    float yValue;

    public event Action OnGrabBowString; 
    public event Action OnReleaseBowString; 
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
    public bool GetIsBowLoaded()
    {
        return bowIsLoaded == 1;
    }

    // Check if character is aiming or not.
    public void SetIsAiming(bool isAiming)
    {
        animator.SetBool(aim, isAiming);
    }

    public void ShootArrow()
    {
        bowIsLoaded = 0;
        animator.SetTrigger(shootArrow);
    }

    public void LoadArrow()
    {
        animator.SetTrigger(reloadArrow);
    }
    // Update character movement to the animator
    public void UpdateAnimationMovement(float moveXValue, float moveYValue)
    {
        xValue = Mathf.MoveTowards(xValue, moveXValue, speedAnimation * Time.deltaTime);
        yValue = Mathf.MoveTowards(yValue, moveYValue, speedAnimation * Time.deltaTime);
        animator.SetFloat(moveX, xValue);
        animator.SetFloat(moveZ, yValue);
    }
    
    // Event for when to go back to non aiming state

    // Event for when to set the Right hand IK


    // Event for spawn arrow
    public void InvokeOnSpawnArrow() => OnSpawnArrow?.Invoke();
    public void InvokeOnSetArrowRightHand() => OnSetArrowRightHand?.Invoke();
    public void InvokeOnGrabBowString() { OnGrabBowString?.Invoke(); }
}
