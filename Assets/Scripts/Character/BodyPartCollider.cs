using UnityEngine;

public class BodyPartCollider : MonoBehaviour
{
    [SerializeField] BodyPart bodyPart;

    [SerializeField] HitDetector hitDetector;

    private void Awake()
    {
        if (!hitDetector)
            hitDetector = GetComponentInParent<HitDetector>();
    }

    public void Hit(int incomingDmg)
    {
        hitDetector.Hit(incomingDmg, bodyPart);
    }
}





