using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] PlayerInput input;
    [SerializeField] ForceReceiver forceReceiver;
    [SerializeField] CharacterRotation characterRotation;
    [SerializeField] Camera camera;
    [SerializeField] Animator animator;
    [SerializeField] float movementSpeed = 10;

    private void Update()
    {
        Vector3 move = CalculateMovement();
        Move(move, movementSpeed);
        UpdateAnimationMovement();
    }

    public void Move(Vector3 motion, float movementSpeed)
    {
        Vector3 movement = motion  * movementSpeed;

        controller.Move((movement + forceReceiver.Movement) * Time.deltaTime);

        if (controller.velocity != Vector3.zero)
        {
            characterRotation.UpdateCharacterRotation();
        }
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

    private void UpdateAnimationMovement()
    {
        if (animator == null) { Debug.LogError("There is no animtor on the gameobject"); return; }

        float moveX = input.MovementValue.x;
        float moveZ = input.MovementValue.y;
        animator.SetFloat("move X", moveX);
        animator.SetFloat("move Y", moveZ);
    }

}
