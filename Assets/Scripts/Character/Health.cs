using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] Stats stat;
    int maxHealth;
    int currentHealth;

    private void Start()
    {
        maxHealth = stat.GetMaxHealthStat;
        currentHealth = maxHealth;
    }

    public int SetMaxHealth(int value) => maxHealth = value;
    public void TakeDamage(int value)
    {
        currentHealth -= Mathf.Clamp(currentHealth, 0, maxHealth);

        if(currentHealth <= 0)
        {
            // Character has died!;
            stat.InvokeCharacterDied();
            // tell the stats script that the character is dead.
        }
    }

    public void Heal(int value)
    {
        currentHealth += Mathf.Clamp(currentHealth, 0, maxHealth);
        
        // Character has healed!
    }
}
