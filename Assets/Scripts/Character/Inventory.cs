using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Sheeths
    [SerializeField] Transform quiverSlotPos;
    [SerializeField] Dictionary<EquipmentType, Transform> sheethPos = new Dictionary<EquipmentType, Transform>();
    // Weapons in their sheeths
    [SerializeField] GameObject selectedWeapon;
    GameObject currentWeaponEquipped;
    public WeaponData GetCurrentWeaponData => currentWeaponEquipped.GetComponent<IWeapon>().GetWeaponData();
    [SerializeField] Dictionary<EquipmentType, GameObject> allWeaponsSpawned = new Dictionary<EquipmentType, GameObject>();


    [SerializeField] int currentWeaponSelectedValue = 0;

    // Current Arrow in quvier
    [SerializeField] Arrow arrowPrefab;
    [SerializeField] Arrow currentArrowLoaded;

    public bool GetIsBowLoaded() { return bowLoaded; }
    [SerializeField] bool bowLoaded = false;

    public Transform GetRightHandPos => rightHandPos;
    [SerializeField] Transform rightHandPos;
    public Transform GetLeftHandPos => leftHandPos;
    [SerializeField] Transform leftHandPos;
    public Transform GetBowStringAcher => bowStringAcher;
    [SerializeField] Transform bowStringAcher;


    // This is for testing.
    // You will get the weapon data and quiver from a shop or picked up somewhere.
    [SerializeField] WeaponData startBow;
    [SerializeField] WeaponData startSword;
    [SerializeField] GameObject quiverData;
    [SerializeField] ArrowData arrowData;


    bool weaponEquipped;

    public event Action OnGrabBowString;
    public event Action OnRealseBowString;
    public event Action<Arrow> OnSetArrowToBowString;
    public event Action<Sprite> OnSelectedWeaponUpdate;

    private void Start()
    {
        if (startBow != null)
        {
            SpawnNewEquipment(startBow);
        }
        if (startSword != null)
        {
            SpawnNewEquipment(startSword);
        }
        if (quiverData != null)
        {
            GameObject quiver = Instantiate(quiverData, quiverSlotPos);
            quiver.transform.localPosition = Vector3.zero;
            quiver.GetComponent<Quiver>().InstertArrowsInQuiver(arrowData);
        }
        CycleWeapons(0);
    }

    private void SpawnNewEquipment(WeaponData weaponData)
    {
        GameObject weapon = Instantiate(weaponData.prefab, leftHandPos);
        weapon.transform.localPosition = Vector3.zero;

        IWeapon newWeapon = weapon.GetComponent<IWeapon>();
        Transform weaponSheeth = GetWeaponSheethTransform(weaponData.equipmentType);

        newWeapon.SpawnWeapon(this, weaponSheeth);
        newWeapon.SetWeaponData(weaponData);

        // Make sure to spawn new weapon and set its parent to the sheeth.
        allWeaponsSpawned.Add(weaponData.equipmentType, weapon);
        newWeapon.Unequip();
    }

    // Gets the sheeth pos
    private Transform GetWeaponSheethTransform(EquipmentType type)
    {
        // Check if sheeth weapons has that equipment type.
        Transform sheeth = sheethPos[type];
        // return sheeth pos
        return sheeth;
    }

    public void ShootArrow(float statsPowerDraw, Transform lookAt, float powerPercent)
    {
        if (currentArrowLoaded != null)
        {
            // get how many arrows are in the quiver. 
            // when shooting, remove an arrow from the quiver.
            IWeapon weapon = currentWeaponEquipped.GetComponent<IWeapon>();
            weapon.PrimartAction(statsPowerDraw, lookAt, powerPercent);
            bowLoaded = false;
        }
    }

    // Try and equip weapon
    public AnimatorOverrideController TryToEquipWeapon()
    {
        // Checking if a weapon is selected
        if (selectedWeapon == null) { return null; }

        // If weapon is equipped and selected weapon doesnt = current weapon, switch weapons.
        if (selectedWeapon != currentWeaponEquipped && weaponEquipped)
        {

            return SwitchWeapons();
        }
        else if (selectedWeapon == currentWeaponEquipped && weaponEquipped)
        {
            // If weapon selected weapon = current weapon, unequip
            UnEquipWeapon();
            return null;
        }
        else
        {
            // If there is no weapon equipped, equip weapon
            return EquipWeapon();
        }
    }
    private AnimatorOverrideController SwitchWeapons()
    {
        IWeapon currentWeapon = currentWeaponEquipped.GetComponent<IWeapon>();
        currentWeapon.Unequip();

        currentWeaponEquipped = selectedWeapon;
        IWeapon newWeapon = currentWeaponEquipped.GetComponent<IWeapon>();
        newWeapon.Equip();
        return newWeapon.GetAnimatorOverrideController();
    }
    private AnimatorOverrideController EquipWeapon()
    {
        weaponEquipped = true;
        currentWeaponEquipped = selectedWeapon;
        IWeapon weapon = currentWeaponEquipped.GetComponent<IWeapon>();
        weapon.Equip();
        return weapon.GetAnimatorOverrideController();
    }
    private void UnEquipWeapon()
    {
        weaponEquipped = false;
        IWeapon weapon = currentWeaponEquipped.GetComponent<IWeapon>();
        weapon.Unequip();
        currentWeaponEquipped = null;
    }


    // Add weapon to list of weapons you are holding or sheethed
    public void AddWeapon(WeaponData newWeapon)
    {
        // Check if there is already that type of equippment filled
        // If so, delete old equippment and replace it with new one
        allWeaponsSpawned.TryGetValue(newWeapon.equipmentType, out GameObject oldWeapon);
        bool weaponWasEquipped = false;

        if (oldWeapon)
        {
            // check if the old weapon = current weapon.  Old weapon is the same equippment type as the new one coming in.
            if (oldWeapon == currentWeaponEquipped) { weaponWasEquipped = weaponEquipped; }

            // check if weapon is equipped.
            if (weaponEquipped)
                UnEquipWeapon();

            // Destroy old weapon and remove from list
            allWeaponsSpawned.Remove(newWeapon.equipmentType);
            Destroy(oldWeapon);

            // spawn new weapon
            SpawnNewEquipment(newWeapon);

            CycleWeapons(0);
            if (weaponWasEquipped)
            {
                selectedWeapon = allWeaponsSpawned[newWeapon.equipmentType];
                EquipWeapon();
            }
        }
        else
        {
            SpawnNewEquipment(newWeapon);
        }
    }


    // Cycle through weapons to equip
    public void CycleWeapons(int value)
    {
        // Determine total weapons available to cylce through
        int weaponCount = allWeaponsSpawned.Count;

        // Guard clause: If there are no weapons, do nothing
        if (weaponCount <= 0) return;

        // Add the cycle direction (+1 or -1)
        currentWeaponSelectedValue += value;

        // Mathematical moduo formula to seamlessly wrap backwards and forwards
        currentWeaponSelectedValue = (currentWeaponSelectedValue % weaponCount + weaponCount) % weaponCount;
        // Safely assign the selected weapon  

        if (currentWeaponSelectedValue < allWeaponsSpawned.Count)
        {
            selectedWeapon = allWeaponsSpawned.Values.ElementAt(currentWeaponSelectedValue);
        }
        OnSelectedWeaponUpdate?.Invoke(selectedWeapon.GetComponent<IWeapon>().GetWeaponData().icon);
    }
    public bool IsBowEquipped()
    {
        if (currentWeaponEquipped)
        {
            EquipmentType type = currentWeaponEquipped.GetComponent<IWeapon>().GetWeaponData().equipmentType;
            if (currentWeaponEquipped.GetComponent<IWeapon>().GetWeaponData().equipmentType == EquipmentType.bow)
            {
                return true;
            }
        }
        return false;
    }
    public bool IsSwordEquipped()
    {
        if (currentWeaponEquipped)
        {
            EquipmentType type = currentWeaponEquipped.GetComponent<IWeapon>().GetWeaponData().equipmentType;
            if (type == EquipmentType.oneHanededWeapon || type == EquipmentType.twoHandedWeapon)
            {
                return true;
            }
        }

        return false;
    }

    // This sets the bow string to the right hand
    public void GrabBowString()
    {
        OnGrabBowString?.Invoke();
    }

    // This removes the string from the right hand
    public void ReleaseBowString()
    {
        OnRealseBowString?.Invoke();
    }

    // Spawns arrow in the right hand from the Quiver.
    public void SpawnArrowInRightHand()
    {
        if (arrowPrefab == null) { Debug.LogError("You must set the arrow Prefab"); }
        currentArrowLoaded = Instantiate(arrowPrefab, rightHandPos.position, Quaternion.identity, rightHandPos);
    }

    // Sets the arrow to be on the bow string
    public void SetArrowToBowString()
    {
        OnSetArrowToBowString?.Invoke(currentArrowLoaded);
        bowLoaded = true;
    }

}
