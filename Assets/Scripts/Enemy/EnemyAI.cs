using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyStats stats = null;

    [Header("Knockback")]
    [SerializeField, Tooltip("higher = slows faster")] private float knockbackDecay = 6f;
    [SerializeField] private float stunOnKnockback = 0.05f;

    private GameObject player;
    private Vector3 knockbackVelocity = Vector3.zero;
    private float stunTimer = 0f;

    private void Awake()
    {
        player = GameManager.Instance.Player.gameObject;
        stats.KnockedBack += ApplyKnockback;
    }

    private void Update()
    {
        Vector3 move = Vector3.zero;
        if (stunTimer > 0f)
        {
            stunTimer -= Time.deltaTime;
        }
        else
        {
            move = GetMovementIntention() * stats.EnemySpeed;
        }

        Vector3 totalVelocity = move + knockbackVelocity;
        transform.position += totalVelocity * Time.deltaTime;
        DecayVelocity();
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

    public void ApplyKnockback(Vector3 fromWorldPos, float force)
    {
        Vector3 dir = (transform.position - fromWorldPos).normalized;

        float resistance = Mathf.Clamp01(stats.KnockbackResistance);
        float appliedForce = force * (1f - resistance);

        knockbackVelocity += dir * appliedForce;
        stunTimer = Mathf.Max(stunTimer, stunOnKnockback);
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


    private void DecayVelocity()
    {
        if (knockbackVelocity.sqrMagnitude > 0.0001f)
        {
            knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, knockbackDecay * Time.deltaTime);
        }
        else
        {
            knockbackVelocity = Vector3.zero;
        }
    }
}