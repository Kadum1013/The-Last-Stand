using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] PlayerInput input;
    [SerializeField] ForceReceiver forceReceiver;
    [SerializeField] Camera camera;
    [SerializeField] CharacterAnimationController animator;

    public bool CheckIfCharacterIsMoving() { return isCharacterMoving; }
    [SerializeField] bool isCharacterMoving;
    private void Update()
    {
        UpdateAnimationMovement();
    }

    public void Move(float movementSpeed)
    {
        Vector3 movement = CalculateMovement() * movementSpeed;

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

    private void UpdateAnimationMovement()
    {
        if (animator == null) { Debug.LogError("There is no animtor on the gameobject"); return; }

        float moveX = input.MovementValue.x;
        float moveZ = input.MovementValue.y;
        animator.UpdateAnimationMovement(moveX, moveZ);

        if(moveX > 0f || moveZ > 0f || moveX < 0f || moveZ < 0f)
        {
            Debug.Log("Character moving!");
        }
        // Checks if character is moving.
        isCharacterMoving = moveX > 0f || moveZ > 0f || moveX < 0f || moveZ < 0f;
    }

}
