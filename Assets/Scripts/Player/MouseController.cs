using UnityEngine;

public class MouseController : MonoBehaviour
{

    private void Start()
    {
        EventListener.Instance.OnLockMouse += LocknHideMouse;
        EventListener.Instance.OnShowMouse += ShowMouse;
    }
    private void OnDisable()
    {
        EventListener.Instance.OnLockMouse -= LocknHideMouse;
        EventListener.Instance.OnShowMouse -= ShowMouse;
    }
    public void LocknHideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
