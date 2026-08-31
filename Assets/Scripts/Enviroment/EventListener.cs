using System;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    public static EventListener Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public event Action OnShoot;
    public event Action OnLockMouse;
    public event Action OnShowMouse;


    public void InvokeOnAttack() => OnShoot?.Invoke();
    public void InvokeOnLockMouse() => OnLockMouse?.Invoke();
    public void InvokeOnShowMouse() => OnShowMouse?.Invoke();
}
