using UnityEngine;

public class StateMachine : MonoBehaviour
{
    State currentState;

    public virtual void OnSceneSwitch()
    {
        currentState?.OnDestroy();
    }

    public void SwitchState(State newState)
    {
        currentState?.Exit();

        currentState = newState;
        currentState.Enter();
    }

    protected virtual void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }

}
