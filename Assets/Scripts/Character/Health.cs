using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] Stats stat;
    int maxHealth;
    [SerializeField] int currentHealth;

    private void Start()
    {
        maxHealth = stat.GetMaxHealthStat;
        currentHealth = maxHealth;
    }

    public int SetMaxHealth(int value) => maxHealth = value;
    public void TakeDamage(int value, BodyPart bodyPart, HitImpactDirection hitImpactDirection)
    {
        currentHealth -= value;
        currentHealth= Mathf.Clamp(currentHealth, 0, maxHealth);
       

        if(currentHealth <= 0)
        {
            // Character has died!;
            stat.InvokeCharacterDied(bodyPart, hitImpactDirection);
            // tell the stats script that the character is dead.
        }
        else
        {
            stat.InvokeHitCharacter(bodyPart, hitImpactDirection);
        }
    }

    public void Heal(int value)
    {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        // Character has healed!
    }
}
