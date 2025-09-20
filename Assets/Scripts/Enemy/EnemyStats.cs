using System;
using UnityEngine;

public class EnemyStats : MonoBehaviour, ItakeDamage
{
    [SerializeField] private int enemyHealth = 5;
    [SerializeField] private int enemyDamage = 10;
    [SerializeField] private int enemySpeed = 2;
    [SerializeField] private int experienceWorth = 50;

    [Header("Defense")]
    [Range(0f, 1f)]
    [SerializeField, Tooltip("0 = full KB, 1 = immune")] 
    private float knockbackResistance = 0f;

    public int EnemyHealth => enemyHealth;
    public int EnemyDamage => enemyDamage;
    public int EnemySpeed => enemySpeed;
    public float KnockbackResistance => knockbackResistance;

    private float currentEnemyHealth;
    private Animator anim;
    public event Action<Vector3, float> KnockedBack;

    private void Awake()
    {
        currentEnemyHealth = enemyHealth;
        anim = GetComponent<Animator>();
    }

    public void OnHit(float damage)
    {
        ApplyDamage(damage);
    }

    public void OnHit(float damage, Vector3 sourceWorldPos, float knockbackForce = 3.5f)
    {
        ApplyDamage(damage);

        if (currentEnemyHealth > 0f)
        {
            KnockedBack?.Invoke(sourceWorldPos, knockbackForce);
        }
    }

    private void ApplyDamage(float damage)
    {
        currentEnemyHealth -= damage;
        anim.SetTrigger("hit");

        if (currentEnemyHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        gameObject.SetActive(false);
        GameManager.Instance.ActiveEnemies.Remove(this.gameObject);
        currentEnemyHealth = enemyHealth;
        DropExpOrHealth();
        RecordKill();
    }

    private void DropExpOrHealth()
    {
        int random = UnityEngine.Random.Range(0, 5);

        if (random == 0)
        {
            HealthUpDrop healthDrop = ObjectPooler.Instance.
                SpawnFromPool("HealthUpDrop", transform.position, transform.rotation).GetComponent<HealthUpDrop>();
            healthDrop.HealthAmount = 10;
        }
        else
        {
            ExperienceDrop experience = ObjectPooler.Instance.
                SpawnFromPool("Experience", transform.position, transform.rotation).GetComponent<ExperienceDrop>();
            experience.ExperienceWorth = experienceWorth;
        }
    }

    private void RecordKill()
    {
        int currentKills = PlayerPrefs.GetInt("EnemiesKilled", 0);
        PlayerPrefs.SetInt("EnemiesKilled", currentKills + 1);
        PlayerPrefs.Save();
    }
}