using UnityEngine;

public class HitDetector : MonoBehaviour
{
    [SerializeField] Health health;
    public void Hit(int dmg, BodyPart bodyPart, HitImpactDirection hitImpactDirection)
    {
        health.TakeDamage(dmg, bodyPart, hitImpactDirection);
    }
}
