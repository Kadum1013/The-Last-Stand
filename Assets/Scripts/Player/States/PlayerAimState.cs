using System.Collections;
using UnityEngine;

public class PlayerAimState : PlayerBaseState
{
    float switchTimer = 0.5f;
    bool switching = false;
    public PlayerAimState(PlayerStatemachine statemachine) : base(statemachine)
    {
    }

    public override void Enter()
    {
        statemachine.PlayerIsDrawingBow();
    }

    public override void Exit()
    {

    }

    public override void OnDestroy()
    {

    }

    public override void Tick(float deltaTime)
    {
        if (!switching)
        {
            // Check if they player has released the attack button and the bow is loaded.
            if (PlayerInput.instance.IsDrawingBow == false && statemachine.GetAnimator().GetIsBowLoaded())
            {
                switching = true;
                statemachine.ShootArrow();
            }
        }

        // Checks to make sure we shot the arrow
        // and the shot animation is done with.
        if(switching && !statemachine.GetAnimator().GetIsShotInProgress())
        {
            statemachine.SwitchState(new PlayerFreeLookState(statemachine));
        }
        statemachine.MoveCharacter();
    }
}
