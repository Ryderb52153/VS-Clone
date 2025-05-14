using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerStats Stats { set; private get; }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyStats enemyStats = collision.gameObject.GetComponent<EnemyStats>();
            int damage = enemyStats.EnemyDamage;
            Stats.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Experience")
        {
            if(collision.TryGetComponent<ExperienceDrop>(out ExperienceDrop exp))
            {
                collision.gameObject.SetActive(false);
                Stats.AddExperience(exp.GetExperienceWorth);
            }
        }
    }
}
