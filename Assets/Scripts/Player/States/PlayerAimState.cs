using UnityEngine;

public class PlayerAimState : PlayerBaseState
{
    public PlayerAimState(PlayerStatemachine statemachine) : base(statemachine)
    {
    }

    public override void Enter()
    {
        EventListener.Instance.OnShoot += statemachine.ShootArrow;
        statemachine.PlayerIsAiming();
    }

    public override void Exit()
    {
        EventListener.Instance.OnShoot -= statemachine.ShootArrow;
    }

    public override void OnDestroy()
    {
       
    }

    public override void Tick(float deltaTime)
    {
        if(PlayerInput.instance.IsAiming == false) { statemachine.SwitchState(new PlayerFreeLookState(statemachine)); }
        statemachine.MoveCharacter();
    }

    // If the player shoots the arrow and the player is still in aiming state.
    // Load a new arrow.
    // While loading, if the player stops aiming.  leave the arrow on the string.
}
