using UnityEngine;

public class Target : MonoBehaviour
{
    public void OnHit(TargetZone zone, Arrow arrow)
    {
        Debug.Log($"Target hit for {zone.GetPoints} Points");
    }
}
