using UnityEngine;

public class DefenseBall : Ability
{
    protected override void Attack()
    {
        base.Attack();

        Vector3 spawnPos = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);
        GameObject gameObject = ObjectPooler.Instance.SpawnFromPool("Defense Ball", spawnPos, transform.rotation);
        gameObject.transform.SetParent(transform);
        DefenseBallBehaviour defenseBallBehaviour = gameObject.GetComponent<DefenseBallBehaviour>();

        defenseBallBehaviour.SetProjectileStats(currentStats, new Vector3(0,0,0));
        defenseBallBehaviour.StartProjectile();
    }
}
