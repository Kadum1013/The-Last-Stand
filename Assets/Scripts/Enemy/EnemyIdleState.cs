using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    float timer = 1;
    
    public EnemyIdleState(EnemyStateMachine statemachine) : base(statemachine)
    {
    }

    public override void Enter()
    {
        statemachine.EquipWeapon();
    }
    public override void Tick(float deltaTime)
    {
        timer -= deltaTime;
        if(timer <= 0)
        {
            statemachine.MoveToTarget();
        }
    }
    public override void Exit()
    {
        
    }

    public override void OnDestroy()
    {
        
    }


}
