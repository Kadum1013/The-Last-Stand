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

    public void ShootArrow(Vector3 direction)
    {
        direction.y += upForce;
        rigidbody.AddForce(direction * speed, ForceMode.Impulse);
        rigidbody.useGravity = true;
    }
}
