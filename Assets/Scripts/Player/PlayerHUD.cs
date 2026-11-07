using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    // Show health

    [SerializeField] Image selectedWeapon;
    public void UpdateSelectedWeapon(Sprite icon)
    {
        selectedWeapon.sprite = icon;
    }
}
