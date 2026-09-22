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


    // Check if character is aiming or not.
    public void SetIsAiming(bool isAiming)
    {
        animator.SetBool(aim, isAiming);
    }

    public void ShootArrow()
    {
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
}
