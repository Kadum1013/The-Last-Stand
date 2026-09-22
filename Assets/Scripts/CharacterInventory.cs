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
    public void ShootArrow()
    {
        if (currentArrowLoaded != null)
        {
            currentBow.ShootArrow();
            bowLoaded = false;
        }
    }

    public void LoadArrow()
    {
       StartCoroutine(GrabArrowFromQuvier());
    }

    private IEnumerator GrabArrowFromQuvier()
    {
        if (arrowPrefab != null)
        {
            yield return new WaitForSeconds(.5f);
            currentArrowLoaded = Instantiate(arrowPrefab, rightHandPos.position, Quaternion.identity, rightHandPos);

            yield return new WaitForSeconds(0.1f);
            currentBow.LoadArrow(currentArrowLoaded, rightHandPos);
            bowLoaded = true;
        }
    }
}
