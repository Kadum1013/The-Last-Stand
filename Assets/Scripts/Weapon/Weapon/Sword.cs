using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] AnimatorOverrideController overrideController;
    [SerializeField] WeaponData weaponData;
    [SerializeField] TypeOfWeapon typeOfWeapon;

    Inventory inventory;
    Transform sheeth;

    public void Equip()
    {
        transform.SetParent(inventory.GetRightHandPos, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Unequip()
    {
        transform.SetParent(sheeth, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    public void PrimartAction(float maxPowerDraw, Transform lookAt, float drawPercent)
    {

    }

    public void SecondaryAction()
    {

    }

    public void SetWeaponData(WeaponData weaponData)
    {
        this.weaponData = weaponData;
    }

    public void SpawnWeapon(Inventory inv, Transform sheeth)
    {
        inventory = inv;
        this.sheeth = sheeth;
    }


    public AnimatorOverrideController GetAnimatorOverrideController()
    {
        return overrideController;
    }

    public TypeOfWeapon GetTypeOfWeapon()
    {
        return typeOfWeapon;
    }

    public WeaponData GetWeaponData()
    {
        return weaponData;
    }

}
