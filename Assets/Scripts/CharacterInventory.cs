using System.Collections;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    // Current Range Weapon The character is holding.
    [SerializeField] Bow currentBow;

    // Current Arrow in quvier
    [SerializeField] Arrow arrowPrefab;
    [SerializeField] Arrow currentArrowLoaded;

    public bool GetIsBowLoaded() { return bowLoaded; }
    [SerializeField] bool bowLoaded = false;
    
    [SerializeField] Transform rightHandPos;

    private void Start()
    {
        if (currentBow != null)
        {
            currentBow.SetRightHandIK(rightHandPos);
        }
    }
    public void ShootArrow(Transform lookAt)
    {
        if (currentArrowLoaded != null)
        {
            currentBow.ShootArrow(lookAt);
            bowLoaded = false;
        }
    }


    // This sets the bow string to the right hand
    public void GrabBowString()
    {
        currentBow.SetHoldingBowString(true);
    }

    // This removes the string from the right hand
    public void ReleaseBowString()
    {
        currentBow.SetHoldingBowString(false);
    }

    // Spawns arrow in the right hand from the Quiver.
    public void SpawnArrowInRightHand()
    {
        if(arrowPrefab == null) { Debug.LogError("You must set the arrow Prefab"); }
        currentArrowLoaded = Instantiate(arrowPrefab, rightHandPos.position, Quaternion.identity, rightHandPos);
    }
    
    // Sets the arrow to be on the bow string
    public void SetArrowToBowString()
    {
        currentBow.LoadArrow(currentArrowLoaded);
        bowLoaded = true;
    }
}
