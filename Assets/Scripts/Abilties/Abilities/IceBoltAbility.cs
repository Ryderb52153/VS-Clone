using UnityEngine;

public class IceBoltAbility : Ability
{
    protected override void UseAbility()
    {
        base.UseAbility();

        GameObject gameObject = ObjectPooler.Instance.SpawnFromPool("Ice Bolt", transform.position, transform.rotation);
        IceBoltProjectileBehaviour iceBolt = gameObject.GetComponent<IceBoltProjectileBehaviour>();

        Vector3 direction = GetBackshotDirection(transform.position);

        if (direction.sqrMagnitude > 0.0001f)
            gameObject.transform.right = direction;

        iceBolt.SetProjectileStats(currentStats, direction);
        iceBolt.StartProjectile();
    }

    private Vector3 GetBackshotDirection(Vector3 playerPos)
    {
        Vector3 move = GameManager.Instance.Player.Movement.RawInputMovement;
        if (move.sqrMagnitude > 0.0001f)
            return (-move).normalized;

        return Vector3.down;
    }
}