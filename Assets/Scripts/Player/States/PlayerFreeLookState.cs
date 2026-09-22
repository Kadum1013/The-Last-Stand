using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    // Free looking state.  Non aiming.

    public PlayerFreeLookState(PlayerStatemachine statemachine) : base(statemachine)
    {
    }

    public override void Enter()
    {
        statemachine.PlayerFreeLook();
    }

    public override void Exit()
    {

    }

    public override void OnDestroy()
    {

    }

    public override void Tick(float deltaTime)
    {
        if (PlayerInput.instance.IsAiming) { statemachine.SwitchState(new PlayerAimState(statemachine)); }

        // Checks if the character is moving. If yes, then turn off right arm IK.
        statemachine.GetRigController().Moving(statemachine.GetCharacterMovement().CheckIfCharacterIsMoving());

        // Moves the character
        statemachine.MoveCharacter();
    }
}
