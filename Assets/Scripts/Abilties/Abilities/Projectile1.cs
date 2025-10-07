using UnityEngine;

public class Projectile1 : Ability
{
    protected override void UseAbility()
    {
        base.UseAbility();

        GameObject gameObject = ObjectPooler.Instance.SpawnFromPool("Projectile 1", transform.position, transform.rotation);
        SquareProjectileBehaviour square = gameObject.GetComponent<SquareProjectileBehaviour>();

        // Shoot in direction of Mouse Position
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector3 direction = (mouseWorldPos - transform.position).normalized;

        // Set Projectile details and start
        square.SetProjectileStats(currentStats, direction);
        square.StartProjectile();
    }
}