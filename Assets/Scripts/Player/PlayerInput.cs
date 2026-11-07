
using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour, InputSystem.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }
    public Vector2 MousePosition { get; private set; }
    public Vector2 ScrollWheelValue { get; private set; }

    public bool IsAiming { get; private set; }
    public bool IsDrawingBow { get; private set; }

    InputSystem inputSystem;
    public static PlayerInput instance;


    public event Action OnPrimartAction;
    public event Action OnEquipAction;
    public event Action<float> OnScrollWheelAction;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        IsAiming = false;
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
        OnPrimartAction?.Invoke();
    }

    public void OnCrouch(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }

    public void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        OnEquipAction?.Invoke();
    }

    public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }

    public void OnLook(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
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

    public void OnLockMouse(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        EventListener.Instance.InvokeOnLockMouse();
    }

    public void OnShowMouse(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        EventListener.Instance.InvokeOnShowMouse();
    }

    public void OnAim(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        IsAiming = context.action.IsPressed();
    }

    public void OnAttackHold(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        IsDrawingBow = context.action.IsPressed();
    }

    public void OnScrollWheel(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Vector2 value = context.ReadValue<Vector2>();
        OnScrollWheelAction?.Invoke(value.y);
    }
}
