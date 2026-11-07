using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine statemachine;

    public EnemyBaseState(EnemyStateMachine statemachine)
    {
        this.statemachine = statemachine;
    }

}
