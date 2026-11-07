using UnityEngine;

public class PlayerSwordAttack : PlayerBaseState
{
    int index; float elapsed; bool queued;
    WeaponData weapon; AttackData atk;
    Animator animator;

    public PlayerSwordAttack(PlayerStatemachine statemachine) : base(statemachine)
    {

    }

    public override void Enter()
    {
        statemachine.GetPlayerInput.OnPrimartAction += Attack;
        weapon = statemachine.GetCharacterInventory().GetCurrentWeaponData;
        animator = statemachine.GetAnimator().GetAnimator;
        Play(0);
    }
    public override void Tick(float deltaTime)
    {
        elapsed += deltaTime * weapon.attackSpeed;
        float t = elapsed / atk.clip.length;

        if (queued && t >= atk.comboOpen && index + 1 < weapon.combo.Length)
            Play(index + 1);
        else if (t >= 1f)
            statemachine.SwitchState(new PlayerFreeLookState(statemachine));
    }
    public override void Exit()
    {
        statemachine.GetPlayerInput.OnPrimartAction -= Attack;
    }

    public override void OnDestroy()
    {
        
    }
    private void Attack()
    {
        queued = true;
    }
    private void Play(int i)
    {
        index = i;
        atk = weapon.combo[index];
        elapsed = 0; queued = false;


        animator.speed = weapon.attackSpeed;
        animator.CrossFadeInFixedTime(atk.clip.name, 0.1f);
    }
}
