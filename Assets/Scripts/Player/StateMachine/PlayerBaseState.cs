using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStatemachine statemachine;
    

    public PlayerBaseState(PlayerStatemachine statemachine)
    {
        this.statemachine = statemachine;
    }
}
