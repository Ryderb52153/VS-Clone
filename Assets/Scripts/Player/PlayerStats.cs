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

    public int CurrentHealth { get => currentHealth; }
    public int Damage {  get => damage; }
    public int CurrentEXP { get => currentEXP; }
    public int MoveSpeed {  get => moveSpeed; }

    public event Action<int> ExpUpdate;
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

        CheckDeath();
    }

    public void AddExperience(int experience)
    {
        currentEXP += experience;
        CheckLevelUp();
        ExpUpdate.Invoke(currentEXP);
    }

    public void AddMaxHealth(int healthToAdd)
    {
        maxHealth += healthToAdd;
        currentHealth += healthToAdd;
    }

    private void CheckLevelUp()
    {
        if(CurrentEXP < 100) { return; }

        LevelUp.Invoke();
        currentEXP -= 100;
    }

    private void CheckDeath()
    {
        if (currentHealth != 0) { return; }

        print("End Game, Character dead");
        isDead = true;
        GameManager.Instance.TriggerGameOver();
    }
}
