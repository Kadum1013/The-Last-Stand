using System;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Base stats (Starting Out)")]
    [SerializeField] int baseArmor = 5;
    [SerializeField] int baseHealth = 100;
    [SerializeField] float baseDrawSpeed = 0.3f;
    [SerializeField] float baseDrawPower = 50;
    [SerializeField] float baseAttackSpeed = 0.5f;
    [SerializeField] float baseAttackPower = 5;

    // current stats of character.
    int currentArmor;
    int maxHealth;
    float currentDrawSpeed;
  [SerializeField]   float currentDrawPower;
    float currentAttackSpeed;
    float currentAttackPower;

    public event Action OnCharacterDied;
    public void InvokeCharacterDied() => OnCharacterDied?.Invoke();

    private void Awake()
    {
        currentArmor = baseArmor;
        maxHealth = baseHealth;
        currentDrawSpeed = baseDrawSpeed;
        currentDrawPower = baseDrawPower;
        currentAttackSpeed = baseAttackSpeed;
        currentAttackPower = baseAttackPower;
    }

    #region Getter for each Stat
    public int GetArmorStat => currentArmor;
    public int GetMaxHealthStat => maxHealth;
    public float GetDrawSpeedStat => currentDrawSpeed;
    public float GetDrawPowerStat => currentDrawPower;
    public float GetAttackSpeedStat => currentAttackSpeed;
    public float GetAttackPowerStat => currentAttackPower;
    #endregion

    #region Setter for each stat
    public void UpdateArmorStat(int newValue) => currentArmor = newValue;
    public void UpdateMaxHealthStat(int newValue) => maxHealth = newValue;
    public void UpdatecurrentDrawSpeedStat(float newValue) => currentDrawSpeed = newValue;
    public void UpdatecurrentDrawPowerStat(float newValue) => currentDrawPower = newValue;
    public void UpdatecurrentAttackSpeedStat(float newValue) => currentAttackSpeed = newValue;
    public void UpdatecurrentAttackPowerStat(float newValue) => currentAttackPower = newValue;
    #endregion

    
}
