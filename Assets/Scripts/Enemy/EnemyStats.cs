using UnityEngine;

public class EnemyStats : MonoBehaviour, ItakeDamage
{
    [SerializeField] private int enemyHealth = 5;
    [SerializeField] private int enemyDamage = 10;
    [SerializeField] private int enemySpeed = 2;

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
            currentEnemyHealth = enemyHealth;
            ObjectPooler.Instance.SpawnFromPool("Experience", transform.position, transform.rotation);
        }
        else
        {
            print("Health Remaining : " + currentEnemyHealth);
        }
    }
}
