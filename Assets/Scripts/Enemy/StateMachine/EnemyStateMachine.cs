using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    [SerializeField] Inventory inventory;
    [SerializeField] Health health;
    public Health GetHealth() => health;

    [SerializeField] Stats stats;
    public Stats GetStats() => stats;

    [SerializeField] CharacterAnimationController animator;
    [SerializeField] EnemyMovement movement;
    [SerializeField] Transform target;

    [SerializeField] List<Collider> colliders = new List<Collider>();

    private void Start()
    {

        SwitchState(new EnemyIdleState(this));
        stats.OnCharacterDied += OnDeath;
        stats.OnCharacterHit += OnHit;
    }
    private void OnDestroy()
    {
        stats.OnCharacterDied -= OnDeath;
        stats.OnCharacterHit -= OnHit;
    }
    private void OnHit(BodyPart bodyPart, HitImpactDirection hitImpactDirection)
    {
        animator.HitAnimation(bodyPart, hitImpactDirection);
        SwitchState(new EnemyImpactState(this));
    }
    private void OnDeath(BodyPart bodyPart, HitImpactDirection hitImpactDirection)
    {
        stats.OnCharacterHit -= OnHit;
        movement.StopAllMovement();
        TurnOffAllColliders();
        animator.Death(bodyPart, hitImpactDirection);
        SwitchState(new EnemyDeathState(this));
    }
    public void MoveToTarget()
    {
        movement.MoveToTarget(target);
    }
    public void EquipWeapon()
    {
        animator.SwitchAnimatorController(inventory.TryToEquipWeapon());

    }
    private void TurnOffAllColliders()
    {
        foreach(Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
