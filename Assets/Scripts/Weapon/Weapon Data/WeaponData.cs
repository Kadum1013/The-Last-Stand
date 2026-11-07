using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon")]
public class WeaponData : ItemData
{
    public int damage;
    public float attackSpeed;
    public GameObject prefab;
    public TypeOfWeapon weaponType;
    public AttackData[] combo; // swing 1, 2, 3
}