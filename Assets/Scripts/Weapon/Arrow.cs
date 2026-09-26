using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] float speed;
    [SerializeField] float upForce;


    public void Start()
    {
        rigidbody.useGravity = false;
    }

    private void FixedUpdate()
    {
        if (rigidbody.linearVelocity.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(rigidbody.linearVelocity.normalized);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        rigidbody.useGravity = false;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.linearVelocity = Vector3.zero;
    }
    public void ShootArrow(Transform lookAt)
    {
        Vector3 dir = lookAt.position - transform.position;
        dir.Normalize();
        dir.y += upForce;

        rigidbody.useGravity = true;

        rigidbody.AddForce(dir * speed, ForceMode.Impulse);

        transform.rotation = Quaternion.LookRotation(dir.normalized);

        Destroy(gameObject, 5f);
    }

    // Test to make sure the arrows is pointing in the right direction.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, transform.forward * 999);
    }
}
