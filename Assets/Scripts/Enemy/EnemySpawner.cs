using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnCooldown = 6f;

    private float cooldownRemaining;

    private float[] spawnXVectorRange = { -.5f, -.25f, .25f, .5f, 1f };
    private string[] enemies = { "Enemy Triangle", "Enemy Purple", "Enemy Red" };

    private void Start()
    {
        GameManager.Instance.endGameDelegate = SpawnEndGame;
    }

    private void TimeTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        cooldownRemaining--;
        if (cooldownRemaining >= 0) { return; }

        SpawnEnemy();
        cooldownRemaining = spawnCooldown;
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomPosition();
        int randomEnemy = Random.Range(0, enemies.Length);
        GameObject enemy = ObjectPooler.Instance.SpawnFromPool(enemies[randomEnemy], new Vector3(spawnPos.x, spawnPos.y, 0), Quaternion.identity);
        GameManager.Instance.ActiveEnemies.Add(enemy);
    }

    private void SpawnEndGame()
    {
        Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(.5f, 1));
        GameObject playerKiller = ObjectPooler.Instance.SpawnFromPool("End Game Boss", new Vector3(spawnPos.x, spawnPos.y, 0), Quaternion.identity);
    }

    private Vector3 GetRandomPosition()
    {
        int randomXVector = Random.Range(0, spawnXVectorRange.Length);
        int randomYVector = Random.Range(0, 1);
        Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(spawnXVectorRange[randomXVector], randomYVector));
        return spawnPos;
    }

    private void OnEnable()
    {
        TimeTickSystem.OnTick += TimeTick;
    }

    private void OnDisable()
    {
        TimeTickSystem.OnTick -= TimeTick;
    }
}
