using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnCooldown = 6f;

    private float cooldownRemaining;

    private void Start()
    {
        GameManager.Instance.endGameDelegate = SpawnEndGame;
    }

    private void Tick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        cooldownRemaining--;

        if (cooldownRemaining < 0)
        {
            SpawnEnemy();
            cooldownRemaining = spawnCooldown;
        }
    }

    public void SpawnEndGame()
    {
        Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(.5f, 1));
        GameObject playerKiller = ObjectPooler.Instance.SpawnFromPool("End Game Boss", new Vector3(spawnPos.x, spawnPos.y, 0), Quaternion.identity);
    }

    // update to spawn in random positions
    // need to come up with an idea for random intervals 
    // of group spawns and single spawns.
    private void SpawnEnemy()
    {
        Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(.5f, 1));
        GameObject enemy = ObjectPooler.Instance.SpawnFromPool("Enemy Triangle", new Vector3(spawnPos.x, spawnPos.y, 0), Quaternion.identity);

        Vector3 spawnPos2 = Camera.main.ViewportToWorldPoint(new Vector3(.5f, 0));
        GameObject enemy2 = ObjectPooler.Instance.SpawnFromPool("Enemy Triangle", new Vector3(spawnPos2.x, spawnPos2.y, 0), Quaternion.identity);
    }

    private void OnEnable()
    {
        TimeTickSystem.OnTick += Tick;
    }

    private void OnDisable()
    {
        TimeTickSystem.OnTick -= Tick;
    }
}
