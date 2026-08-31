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

    private void OnTriggerEnter(Collider other)
    {
        rigidbody.useGravity = false;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.linearVelocity = Vector3.zero;
    }
    public void ShootArrow()
    {
        Vector3 dir = transform.forward;
        dir.y += upForce;
        rigidbody.AddForce(dir * speed, ForceMode.Impulse);
        rigidbody.useGravity = true;
        Destroy(gameObject, 5f);
    }
}
