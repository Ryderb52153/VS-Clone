using UnityEngine;

public class Projectile1 : Ability
{
    protected override void Attack()
    {
        base.Attack();

        // Spawn Projectile from Pooler
        GameObject gameObject = ObjectPooler.Instance.SpawnFromPool("Projectile 1", transform.position, transform.rotation);
        SquareProjectileBehaviour square = gameObject.GetComponent<SquareProjectileBehaviour>();

        // Shoot in direction of Mouse Position
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector3 direction = (mouseWorldPos - transform.position).normalized;

        // Set Projectile details
        square.SetSpeed = currentStats.moveSpeed;
        square.SetLifeSpan = currentStats.lifeSpan;
        square.SetCooldown = currentStats.cooldown;
        square.SetDamage = currentStats.damage;
        square.SetDirection = direction;
        square.StartProjectile();
    }
}
