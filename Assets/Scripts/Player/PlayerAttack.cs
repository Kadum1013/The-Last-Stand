using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Bow bow;

    private void Start()
    {
        EventListener.Instance.OnShoot += TryToShootArrow;
    }

    private void TryToShootArrow()
    {
        bow.FireArrow();
    }
}
