using UnityEngine;

public class PlayerStatemachine : StateMachine
{
    // Character rotation handles the rotation of the player, where to look.
    public CharacterRotation GetCharacterRotation() {  return characterRotation; }
    [SerializeField] CharacterRotation characterRotation;

    // Character Movement, handles character movement and speed.
    public CharacterMovement GetCharacterMovement() { return characterMovement; }
    [SerializeField] CharacterMovement characterMovement;

    // Rig controller
    public RigController GetRigController() { return rigController; }
    [SerializeField] RigController rigController;

    public CharacterInventory GetCharacterInventory() { return inventory; }
    [SerializeField] CharacterInventory inventory;

    [Header("Camera controller.")]
    // Controller that handles the cinemachine cameras.
    [SerializeField] CameraController cameraController;

    [Header("Character animator controller that handles all animation.")]
    [SerializeField] CharacterAnimationController animator;

    bool isAiming = false;

    // Character movement speed.
    [SerializeField] float normalMovementSpeed = 10;
    [SerializeField] float aimingMovementSpeed = 10;

    [SerializeField] float reloadSpeed = 1f;

    private void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
    }
    private void OnEnable()
    {
        animator.OnGrabBowString += GrabBowString;
        animator.OnReleaseBowString += ReleaseBowString;
        animator.OnSpawnArrow += SpawnArrow;
        animator.OnSetArrowRightHand += SetArrowToBowString;
    }
    private void OnDisable()
    {
        animator.OnGrabBowString -= GrabBowString;
        animator.OnReleaseBowString -= ReleaseBowString;
        animator.OnSpawnArrow -= SpawnArrow;
        animator.OnSetArrowRightHand -= SetArrowToBowString;
    }
    public void GrabBowString()
    {
        rigController.EnableRightHandIK();
        inventory.GrabBowString();
    }
    public void ReleaseBowString()
    {
        rigController.DisableRightHandIK();
        inventory.ReleaseBowString();
    } 


    // Character movement.
    public void MoveCharacter()
    {
        if (isAiming)
        {
            characterMovement.Move(aimingMovementSpeed);
        }
        else
        {
            characterMovement.Move(normalMovementSpeed);
        }

    }

    // Player is aiming
    public void PlayerIsAiming()
    {
        isAiming = true;
        LoadBow();
        GetCharacterRotation().SetIsAiming(true);
        animator.SetIsAiming(true);
        cameraController.SetAiming(true);
        if (rigController != null)
        {
            rigController.Aiming();
        }
    }


    // Player is not aiming and is in free look.
    public void PlayerFreeLook()
    {
        isAiming = false;
        GetCharacterRotation().SetIsAiming(false);
        animator.SetIsAiming(false);
        cameraController.SetAiming(false);
        if (rigController != null)
        {
            rigController.NotAiming();
        }
    }

    // Try and shoot an arrow.
    public void ShootArrow()
    {
        inventory.ShootArrow(characterRotation.GetTargetLookAtTransform());
        ReleaseBowString();
        animator.ShootArrow();
        LoadBow();
    }

    public void LoadBow()
    {
        if (inventory.GetIsBowLoaded()) { return; }
        animator.LoadArrow();
    }

    private void SpawnArrow()
    {
        inventory.SpawnArrowInRightHand();
    }
    private void SetArrowToBowString()
    {
        inventory.SetArrowToBowString();
    }
}
