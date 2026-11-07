using System.Collections;
using UnityEngine;

public class PlayerAimState : PlayerBaseState
{
    bool switching = false;
    bool aimShaking = false;
    float holdingTime = 3f;
    float currentTime = 0;
    WeaponData weapon;
    Animator animator;
    public PlayerAimState(PlayerStatemachine statemachine) : base(statemachine)
    {
    }

    public override void Enter()
    {
        animator = statemachine.GetAnimator().GetAnimator;
        weapon = statemachine.GetCharacterInventory().GetCurrentWeaponData;
        statemachine.PlayerIsDrawingBow();
    }
    public override void Tick(float deltaTime)
    {
        currentTime += deltaTime;
        if(currentTime >= holdingTime && !aimShaking && statemachine.GetAnimator().GetIsShotInProgress())
        {
            aimShaking = true;
            animator.CrossFadeInFixedTime(weapon.combo[2].clip.name, 0.1f);
            statemachine.GetAnimator().SetAimShacking(true);
        }

        statemachine.GetCameraController().SetAiming(PlayerInput.instance.IsAiming);


        if (!switching)
        {
            // Check if they player has released the attack button and the bow is loaded.
            if (PlayerInput.instance.IsDrawingBow == false && statemachine.GetAnimator().GetIsBowLoaded)
            {
                switching = true;
                statemachine.ShootArrow();
            }
        }

        if (statemachine.GetAnimator().GetIsShotInProgress())
        {
            statemachine.GetCrosshair().UpdateIsAiming(true);
            statemachine.GetCrosshair().ShrinkCircle(statemachine.GetAnimator().GetDrawProgress());
        }
        // Checks to make sure we shot the arrow
        // and the shot animation is done with.
        if (switching && !statemachine.GetAnimator().GetIsShotInProgress())
        {
            statemachine.SwitchState(new PlayerFreeLookState(statemachine));
        }
        statemachine.MoveCharacter();
    }
    public override void Exit()
    {
        Debug.Log("Exinting aim");
        statemachine.GetCrosshair().UpdateIsAiming(false);
        statemachine.GetCameraController().SetAiming(false);
        statemachine.GetAnimator().SetAimShacking(false);
    }

    public override void OnDestroy()
    {

    }

   
}
