using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour, InputSystem.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }

    InputSystem inputSystem;
    public static PlayerInput instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        inputSystem = new InputSystem();
        inputSystem.Player.SetCallbacks(this);

        inputSystem.Enable();
    }
    public void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        EventListener.Instance.InvokeOnAttack();
    }

    public void OnCrouch(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
    }

    public void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
    }

    public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
    }

    public void OnLook(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
    }

    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnNext(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
    }

    public void OnPrevious(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
    }

    public void OnSprint(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
    }
}
