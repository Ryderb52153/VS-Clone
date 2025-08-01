using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyStats stats = null;

    private GameObject player;

    private void Awake()
    {
        player = GameManager.Instance.Player.gameObject;
    }

    private void Update()
    {
        transform.position += GetMovementIntention() * stats.EnemySpeed * Time.deltaTime;
    }

    public Vector3 GetMovementIntention()
    {
        Vector3 intention = Vector3.zero;

        intention += MoveTowardsPlayer(intention);

        intention += PushAwayFromOtherEnemies(intention);

        // If we're really close just stop.
        if (intention.magnitude < 0.05f)
        {
            return Vector3.zero;
        }

        return intention.normalized;
    }

    private Vector3 MoveTowardsPlayer(Vector3 intention)
    {
        Vector3 direction = GetDirection(this.gameObject, player);
        float distance = GetDistance(this.gameObject, player);

        float targetDistance = .9f;
        float springStrength = (distance - targetDistance);
        intention += direction * springStrength;
        return intention;
    }

    private Vector3 PushAwayFromOtherEnemies(Vector3 intention)
    {
        foreach (GameObject activeEnemy in GameManager.Instance.ActiveEnemies)
        {
            if (activeEnemy == this.gameObject) { continue; }

            Vector3 direction = GetDirection(this.gameObject, activeEnemy);
            float distance = GetDistance(this.gameObject, activeEnemy);

            float springStrength = 1f / (1f + distance * distance * distance); // Inverse cube of distance
            intention -= direction * springStrength;
        }

        return intention;
    }

    private Vector3 GetDirection(GameObject enemy, GameObject player)
    {
        return (player.transform.position - enemy.transform.position).normalized;
    }

    private float GetDistance(GameObject enemy, GameObject player)
    {
        return Vector3.Distance(enemy.transform.position, player.transform.position);
    }
}
