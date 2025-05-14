using UnityEngine;

public class Projectile1 : Ability
{
    protected override void Attack()
    {
        base.Attack();

        // Spawn Projectile from Pooler
        GameObject gameObject = ObjectPooler.Instance.SpawnFromPool("Projectile 1", transform.position, transform.rotation);
        SquareProjectileBehaviour square = gameObject.GetComponent<SquareProjectileBehaviour>();

        // Random Direction
        int randomY = Random.Range(-1, 2);
        int randomX = Random.Range(-1, 2);
        Vector3 direction = new Vector3(randomX, randomY, 0);

        if (direction.y == 0 && direction.x == 0)
            direction.y = -1;

        // Set Projectile details
        square.SetSpeed = currentStats.moveSpeed;
        square.SetLifeSpan = currentStats.lifeSpan;
        square.SetCooldown = currentStats.cooldown;
        square.SetDamage = currentStats.damage;
        square.SetDirection = direction;
        square.StartProjectile();
    }
}
