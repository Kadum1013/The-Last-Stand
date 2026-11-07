using System;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    // Free looking state.  Non aiming.
    bool alreadySwitchedHolding = false;
    
    public PlayerFreeLookState(PlayerStatemachine statemachine) : base(statemachine)
    {
    }

    public override void Enter()
    {
        statemachine.PlayerFreeLook();
        statemachine.GetPlayerInput.OnEquipAction += statemachine.TryToEquipWeapon;
        statemachine.GetPlayerInput.OnPrimartAction += Attack;
    }



    public override void Exit()
    {
        statemachine.GetPlayerInput.OnEquipAction -= statemachine.TryToEquipWeapon;
        statemachine.GetPlayerInput.OnPrimartAction -= Attack;
    }

    public override void OnDestroy()
    {

    }

    public override void Tick(float deltaTime)
    {
        if (PlayerInput.instance.IsDrawingBow && statemachine.GetCharacterInventory().IsBowEquipped()) { statemachine.SwitchState(new PlayerAimState(statemachine)); }

        // Moves the character
        statemachine.MoveCharacter();

        if (!statemachine.GetAnimator().GetIsBowLoaded)
        {
            ReleaseString();
            return;
        }
        // Checks if the character is moving. If yes, then turn off right arm IK.
        statemachine.GetRigController().Moving(statemachine.GetCharacterMovement().CheckIfCharacterIsMoving());


        if (statemachine.GetCharacterMovement().CheckIfCharacterIsMoving())
        {
            ReleaseString();
        }
        else
        {
            GrabString();
        }
    }

    private void GrabString()
    {
        if (!alreadySwitchedHolding)
        {
            statemachine.GrabBowString();
            alreadySwitchedHolding = true;
        }
    }
    private void ReleaseString()
    {
        if (alreadySwitchedHolding)
        {
            statemachine.ReleaseBowString();
            alreadySwitchedHolding = false;
        }
    }
    private void Attack()
    {
        if (statemachine.GetCharacterInventory().IsSwordEquipped())
        {
            statemachine.SwitchState(new PlayerSwordAttack(statemachine));
        }
    }
}
