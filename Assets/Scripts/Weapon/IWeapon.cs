using UnityEngine;

public interface IWeapon
{
    AnimatorOverrideController GetAnimatorOverrideController();
    void SpawnWeapon(Inventory inv, Transform sheeth);
    void Equip();
    void Unequip();
    void PrimartAction(float maxPowerDraw, Transform lookAt, float drawPercent); // Sword slash / Bow shoot
    void SecondaryAction(); // Sword block / heavy shot
    TypeOfWeapon GetTypeOfWeapon();
    WeaponData GetWeaponData();
    void SetWeaponData(WeaponData weaponData);
}
