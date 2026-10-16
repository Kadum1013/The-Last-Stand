using System;
using System.Collections;
using UnityEngine;

public class PlayerStatemachine : StateMachine
{
    // Crosshair as well as precision ring.
    public Crosshair GetCrosshair() { return crosshair; }
    [SerializeField] Crosshair crosshair;

    // Character rotation handles the rotation of the player, where to look.
    public PlayerRotation GetCharacterRotation() { return characterRotation; }
    [SerializeField] PlayerRotation characterRotation;

    // Character Movement, handles character movement and speed.
    public PlayerMovement GetCharacterMovement() { return characterMovement; }
    [SerializeField] PlayerMovement characterMovement;

    // Rig controller
    public RigController GetRigController() { return rigController; }
    [SerializeField] RigController rigController;

    public PlayerInventory GetCharacterInventory() { return inventory; }
    [SerializeField] PlayerInventory inventory;

    [Header("Camera controller.")]
    // Controller that handles the cinemachine cameras.
    [SerializeField] CameraController cameraController;

    [Header("Character animator controller that handles all animation.")]
    [SerializeField] CharacterAnimationController animator;
    public CharacterAnimationController GetAnimator() { return animator; }


    [Header("Players Input System")]
    [SerializeField] PlayerInput input;
    public PlayerInput GetPlayerInput => input;

    [Header("Stats")]
    [SerializeField] Stats stats;
    public Stats GetStats => stats;

    bool isAiming = false;

    // Character movement speed.
    [SerializeField] float normalMovementSpeed = 10;
    [SerializeField] float aimingMovementSpeed = 5;


    private void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
    }
    private void OnEnable()
    {
        animator.OnGrabBowString += GrabBowString;
        animator.OnSpawnArrow += SpawnArrow;
        animator.OnSetArrowRightHand += SetArrowToBowString;
        stats.OnCharacterDied += CharacterDied;
    }

    

    private void OnDisable()
    {
        animator.OnGrabBowString -= GrabBowString;
        animator.OnSpawnArrow -= SpawnArrow;
        animator.OnSetArrowRightHand -= SetArrowToBowString;
        stats.OnCharacterDied -= CharacterDied;
    }
    private void CharacterDied()
    {
        // Switch to death state.
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

    // Player is drawing the bow back.
    public void PlayerIsDrawingBow()
    {
        // Release bow string hand IK
        ReleaseBowString();
        isAiming = true;
        
        GetCharacterRotation().SetIsAiming(true);

        // Sets the draw speed Animation
        animator.SetDrawSpeed(stats.GetDrawSpeedStat);
        animator.SetIsAiming(true);

        // Load the bow with a new arrow
        LoadBow();

        // This will be an ablity you can unlock
        //cameraController.SetAiming(true);
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

        //  This will be an ablity you can unlock
        // cameraController.SetAiming(false);
        if (rigController != null)
        {
            rigController.NotAiming();
        }
    }

    // Try and shoot an arrow.
    public void ShootArrow()
    {
        animator.ShootArrow();
        ReleaseBowString();
        inventory.ShootArrow(stats.GetDrawPowerStat, characterRotation.GetTargetLookAtTransform(), crosshair.GetPowerMultiplier);
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
