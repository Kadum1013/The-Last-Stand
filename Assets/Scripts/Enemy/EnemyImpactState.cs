using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{

    float hitAnimationTimer = 1f;
    public EnemyImpactState(EnemyStateMachine statemachine) : base(statemachine)
    {

    }

    public override void Enter()
    {
    }
    public override void Tick(float deltaTime)
    {
        hitAnimationTimer -= deltaTime;

        if(hitAnimationTimer <= 0)
        {
            statemachine.SwitchState(new EnemyIdleState(statemachine));
        }
    }

    public override void Exit()
    {

    }

    public override void OnDestroy()
    {
        
    }

}
