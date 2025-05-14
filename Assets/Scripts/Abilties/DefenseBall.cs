using UnityEngine;

public class DefenseBall : Ability
{
    protected override void Attack()
    {
        base.Attack();

        // Spawn Projectile from Pooler
        Vector3 spawnPos = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);

        GameObject gameObject = ObjectPooler.Instance.SpawnFromPool("Defense Ball", spawnPos, transform.rotation);
        gameObject.transform.SetParent(transform);
        DefenseBallBehaviour defenseBallBehaviour = gameObject.GetComponent<DefenseBallBehaviour>();

        // Set details
        defenseBallBehaviour.SetSpeed = currentStats.moveSpeed;
        defenseBallBehaviour.SetCooldown = currentStats.cooldown;
        defenseBallBehaviour.SetDamage = currentStats.damage;
        defenseBallBehaviour.SetLifeSpan = currentStats.lifeSpan;
        defenseBallBehaviour.StartProjectile();
    }
}
