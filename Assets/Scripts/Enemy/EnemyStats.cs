using UnityEngine;

public class EnemyStats : MonoBehaviour, ItakeDamage
{
    [SerializeField] private int enemyHealth = 5;
    [SerializeField] private int enemyDamage = 10;
    [SerializeField] private int enemySpeed = 2;
    [SerializeField] private int experienceWorth = 50;

    public int EnemyHealth { get { return enemyHealth; } }
    public int EnemyDamage { get { return enemyDamage; } }
    public int EnemySpeed { get { return enemySpeed; } }

    private float currentEnemyHealth;

    private void Awake()
    {
        currentEnemyHealth = enemyHealth;
    }

    public void OnHit(float damage)
    {
        currentEnemyHealth -= damage;

        if (currentEnemyHealth <= 0)
        {
            gameObject.SetActive(false);
            GameManager.Instance.ActiveEnemies.Remove(this.gameObject);
            currentEnemyHealth = enemyHealth;

            DropExpOrHealth();
        }
        else
        {
            //print("Health Remaining : " + currentEnemyHealth);
        }
    }

    private void DropExpOrHealth()
    {
        int random = Random.Range(0, 5);

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
}
