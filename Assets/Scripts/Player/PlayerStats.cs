using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int damage = 10;
    [SerializeField] private int moveSpeed = 10;

    private int currentHealth;
    private int currentEXP;
    private bool isDead = false;

    private int expLevels = 100;
    //private int[] expLevels = { 150, 220, 320, 450, 510, 620, 820, 970, 1150, 1360};

    public int CurrentHealth { get => currentHealth; }
    public int Damage {  get => damage; }
    public int CurrentEXP { get => currentEXP; }
    public int MoveSpeed {  get => moveSpeed; }
    public int MaxHealth { get => maxHealth; }

    public event Action<int, int> ExpUpdate;
    public event Action<int, int> HealthUpdate;
    public event Action LevelUp;

    private void Start()
    {
        currentHealth = maxHealth;
        currentEXP = 0;
    }

    public void TakeDamage(int damage)
    {
        if(isDead) return;

        if (currentHealth > 0)
            currentHealth -= damage;
        else if (currentHealth < 0)
            currentHealth = 0;

        HealthUpdate?.Invoke(maxHealth,currentHealth);

        CheckDeath();
    }

    public void AddExperience(int experience)
    {
        currentEXP += experience;
        CheckLevelUp();
        ExpUpdate.Invoke(currentEXP, expLevels);
    }

    public void AddMaxHealth(int healthToAdd)
    {
        maxHealth += healthToAdd;
        currentHealth += healthToAdd;
        HealthUpdate?.Invoke(maxHealth, currentHealth);
    }

    private void CheckLevelUp()
    {
        if(currentEXP < expLevels) { return; }

        LevelUp.Invoke();
        currentEXP -= expLevels;
        expLevels += 10;
    }

    private void CheckDeath()
    {
        if (currentHealth != 0) { return; }

        isDead = true;
        GameManager.Instance.TriggerGameOver();
    }
}
