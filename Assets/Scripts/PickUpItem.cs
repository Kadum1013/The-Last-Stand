using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;

    private void OnTriggerEnter(Collider other)
    {
        Inventory playerInv;
        if (other.TryGetComponent<Inventory>(out playerInv))
        {
            playerInv.AddWeapon(weaponData);
            Destroy(gameObject);
        }
    }
}
