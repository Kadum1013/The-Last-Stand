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


    public void InvokeOnAttack() => OnShoot?.Invoke();
}
