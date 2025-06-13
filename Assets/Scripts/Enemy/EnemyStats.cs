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

            HealthUpDrop experience = ObjectPooler.Instance.SpawnFromPool
                ("HealthUpDrop", transform.position, transform.rotation).GetComponent<HealthUpDrop>();
                experience.HealthAmount = 10;

            /*            ExperienceDrop experience = ObjectPooler.Instance.SpawnFromPool
                            ("Experience", transform.position, transform.rotation).GetComponent<ExperienceDrop>();
                        experience.ExperienceWorth = experienceWorth;*/
        }
        else
        {
            //print("Health Remaining : " + currentEnemyHealth);
        }
    }
}
