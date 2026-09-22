
using UnityEngine;

public class PlayerInput : MonoBehaviour, InputSystem.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }
    public Vector2 MousePosition { get; private set; }

    public bool IsAiming { get; private set; }

    InputSystem inputSystem;
    public static PlayerInput instance;

    [SerializeField] bool testAiming;
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

        IsAiming = false;
    }

    private void Update()
    {
        if (testAiming)
        {
            IsAiming = true;
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
        if(!context.performed) return;
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
}
