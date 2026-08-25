using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] PlayerInput input;
    [SerializeField] ForceReceiver forceReceiver;
    [SerializeField] Camera camera;
    [SerializeField] float movementSpeed = 10;

    private void Update()
    {
        Vector3 move = CalculateMovement();
        Move(move, movementSpeed);
    }

    public void Move(Vector3 motion, float movementSpeed)
    {
        Vector3 movement = motion  * movementSpeed;

        controller.Move((movement + forceReceiver.Movement) * Time.deltaTime);
    }

    protected Vector3 CalculateMovement()
    {
        Vector3 foward = camera.transform.forward;
        Vector3 right = camera.transform.right;

        foward.y = 0f;
        right.y = 0f;

        foward.Normalize();
        right.Normalize();

        return foward * input.MovementValue.y +
           right * input.MovementValue.x;
    }
    // Movement, Take in move values and speed.

}
