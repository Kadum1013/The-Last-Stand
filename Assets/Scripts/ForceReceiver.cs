using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(CharacterController))]
public class ForceReceiver : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] private float drag = 0.3f;
    private Vector3 dampingVelocity;
    private Vector3 impact;
    float verticalVelocity;
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Update()
    {

        if (verticalVelocity < 0f && characterController.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;

        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }
}
