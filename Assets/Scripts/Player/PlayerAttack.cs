using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Bow bow;
    [SerializeField] Vector3 targetDir;

    private void Start()
    {
        EventListener.Instance.OnShoot += TryToShootArrow;
    }

    private void TryToShootArrow()
    {
        bow.FireArrow();
    }
    public void SetLookAtDirection(Vector3 dir)
    {
        targetDir = dir;
    }
}
