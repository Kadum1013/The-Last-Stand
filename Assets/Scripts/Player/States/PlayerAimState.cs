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
            if (PlayerInput.instance.IsDrawingBow == false && statemachine.GetBowIsLoaded())
            {
                // If player lets go of attack and the bow isnt loaded.
                // Wait for the bool bow is loaded to shoot the arrow. 
                // If player trys and holds down the attack again before the bow is loaded
                // The arrow wont fire, it will be like they never let go of attack.
                switching = true;
                statemachine.ShootArrow();
                statemachine.SwitchState(new PlayerFreeLookState(statemachine));
            }

        }

        statemachine.MoveCharacter();
    }
}
