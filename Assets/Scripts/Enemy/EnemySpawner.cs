using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnCooldown = 6f;

    private float cooldownRemaining;

    private float[] spawnXVectorRange = { -.5f, -.25f, .25f, .5f, 1f };

    private void Start()
    {
        GameManager.Instance.endGameDelegate = SpawnEndGame;
    }

    private void TimeTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        cooldownRemaining--;

        if (cooldownRemaining < 0)
        {
            int randomXNum = Random.Range(0, spawnXVectorRange.Length);
            int randomYVector = Random.Range(0, 1);

            SpawnEnemy(spawnXVectorRange[randomXNum], randomYVector);
            cooldownRemaining = spawnCooldown;
        }
    }

    public void SpawnEndGame()
    {
        Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(.5f, 1));
        GameObject playerKiller = ObjectPooler.Instance.SpawnFromPool("End Game Boss", new Vector3(spawnPos.x, spawnPos.y, 0), Quaternion.identity);
    }

    private void SpawnEnemy(float randomVectorX, int randomVectorY)
    {
        Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(randomVectorX, randomVectorY));
        GameObject enemy = ObjectPooler.Instance.SpawnFromPool("Enemy Triangle", new Vector3(spawnPos.x, spawnPos.y, 0), Quaternion.identity);
        GameManager.Instance.ActiveEnemies.Add(enemy);
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
