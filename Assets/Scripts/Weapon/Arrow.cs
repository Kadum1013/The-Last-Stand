using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    [Tooltip("How much the arrow will go into an object")]
    [SerializeField] float embedDepth = 0.15f;
    [SerializeField] float baseDmg = 1;
    [SerializeField] float upForce = 0.1f; // lift at the reference power
    [SerializeField] float referencePower = 50f;    // draw power where upForce feels right
    [SerializeField] float liftFalloff = 2f;        // 2 = physically accurate, 1 = gentler change
    [SerializeField] float maxLiftMultiplier = 4f;  // cap so very weak draws don't lob too high
    [SerializeField] TrailRenderer trail;
    int currentDamage;

    bool hasHit = false;
    Vector3 flightDirection;

    public void Start()
    {
        rigidbody.useGravity = false;
    }

    private void FixedUpdate()
    {
        if (rigidbody.linearVelocity.sqrMagnitude > 0.01f)
        {
            flightDirection = rigidbody.linearVelocity.normalized;
            transform.rotation = Quaternion.LookRotation(rigidbody.linearVelocity.normalized);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;

        hasHit = true;

        // Finds the target on the collider's object or any parent
        Target target = collision.collider.GetComponentInParent<Target>();
        if (target != null)
        {
            TargetZone targetZone = collision.collider.GetComponent<TargetZone>();
            target.OnHit(targetZone, this);
        }

        BodyPartCollider bodypartCollider = collision.collider.GetComponent<BodyPartCollider>();
        if(bodypartCollider != null)
        {
            bodypartCollider.Hit(currentDamage);
        }


        StickIntoSurface(collision);
    }

    private void StickIntoSurface(Collision collision)
    {
        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.isKinematic = true;
        rigidbody.detectCollisions = false;

        // Undo any rotation the collision applied.
        transform.rotation = Quaternion.LookRotation(flightDirection);

        // push the arrow deeper along its flight path, then parent it
        transform.position += flightDirection * embedDepth;


        // Only parent to objects are a target or have a rigidbody attached to it.
        if (collision.collider.attachedRigidbody != null ||
    collision.collider.GetComponentInParent<Target>() != null)
        {
            transform.SetParent(collision.collider.transform, true);
        }
    }

    public void ShootArrow(float dmg, float launchPower, Transform lookAt)
    {
        currentDamage = Mathf.RoundToInt(dmg + baseDmg);
        // Get direction to shoot the arrow
        Vector3 dir = lookAt.position - transform.position;
        dir.Normalize();

        // More lift when the draw is weak, less when it's strong
        float ratio = referencePower / Mathf.Max(launchPower, 0.01f);
        float lift = upForce * Mathf.Min(Mathf.Pow(ratio, liftFalloff), maxLiftMultiplier);
        dir.y += lift;
        dir.Normalize(); // keeps the lift from changing the arrow's speed

        rigidbody.centerOfMass = new Vector3(0, 0, 0.5f);
        rigidbody.useGravity = true;
        rigidbody.AddForce(dir * launchPower, ForceMode.Impulse);

        trail.enabled = true;

        flightDirection = dir;
        transform.rotation = Quaternion.LookRotation(dir);

        Destroy(gameObject, 5f);
    }
}
