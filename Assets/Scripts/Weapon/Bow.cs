using Unity.VisualScripting;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] Arrow arrowPrefab;
    [SerializeField] Transform spawnArrowPos;

    // Cache to hold current Arrow that is loaded.
    Arrow currentArrow;
    private void Start()
    {
        // At the start, spawn an arrow.
        SpawnArrow();
    }
    public void SpawnArrow()
    {
        // Creates arrow and attaches it to the bow
        currentArrow = Instantiate(arrowPrefab, spawnArrowPos.position, spawnArrowPos.rotation, spawnArrowPos);
    }
    public void FireArrow()
    {
        // If there isnt an arrow ready to shoot, skip
        if (currentArrow != null)
        {
            // Detache it from the bow gameobject.
            currentArrow.transform.SetParent(null);

            currentArrow.ShootArrow(transform.forward);

            // Destory arrow after 5 seconds.
            Destroy(currentArrow.gameObject, 5f);

            SpawnArrow();
        }
    }
}
