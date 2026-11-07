using System.Collections.Generic;
using UnityEngine;

public class Quiver : MonoBehaviour
{
    [SerializeField] List<Transform> arrowSlots = new List<Transform>();

    
    public void InstertArrowsInQuiver(ArrowData arrowData)
    {
        foreach(Transform slot in arrowSlots)
        {
            GameObject arrow = Instantiate(arrowData.arrowInstert, slot);
        }
    }
}
