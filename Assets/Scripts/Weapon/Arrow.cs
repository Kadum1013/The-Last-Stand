using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] float speed;
    [SerializeField] float upForce;
    [SerializeField] float embedDepth = 0.15f;
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

    public void ShootArrow(Transform lookAt)
    {
        // Get direction to shoot the arrow
        Vector3 dir = lookAt.position - transform.position;
        dir.Normalize();

        // Add a little up force to it
        dir.y += upForce;

        rigidbody.useGravity = true;
        rigidbody.AddForce(dir * speed, ForceMode.Impulse);

        // Set the flight direction so it knows which direction it is facing
        // for when it sticks to objects that are really close.
        flightDirection = dir.normalized;
        rigidbody.linearVelocity = flightDirection * speed;

        transform.rotation = Quaternion.LookRotation(dir.normalized);

        // Destory the arrow after a set time.
        Destroy(gameObject, 5f);
    }

    // Test to make sure the arrows is pointing in the right direction.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, transform.forward * 999);
    }
}
