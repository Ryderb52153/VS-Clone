using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPhase> spawnPhases;

    private int cooldownRemaining = 0;
    private int currentPhaseIndex = 0;

    private SpawnPhase CurrentPhase => spawnPhases[currentPhaseIndex];
    private float[] spawnXVectorRange = { -.5f, -.25f, .25f, .5f, 1f };

    private void Start()
    {
        GameManager.Instance.endGameDelegate = SpawnEndGame;
        GameManager.Instance.Player.Stats.LevelUp += IncreasePhase;
    }

    private void TimeTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        cooldownRemaining--;
        if(cooldownRemaining > 0) { return; }

        SpawnEnemy();
        cooldownRemaining = CurrentPhase.spawnCooldown;
    }

    private void IncreasePhase()
    {
        if (currentPhaseIndex > spawnPhases.Count - 2) { return; }
        currentPhaseIndex++;
        cooldownRemaining = 0;
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomPosition();
        string[] availableEnemies = CurrentPhase.enemyTypes;

        int randomEnemy = Random.Range(0, availableEnemies.Length);
        GameObject enemy = ObjectPooler.Instance.SpawnFromPool(availableEnemies[randomEnemy], new Vector3(spawnPos.x, spawnPos.y, 0), Quaternion.identity);
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
        if (GameManager.Instance != null) 
            GameManager.Instance.Player.Stats.LevelUp += IncreasePhase;

        TimeTickSystem.OnTick += TimeTick;
    }

    private void OnDisable()
    {
        GameManager.Instance.Player.Stats.LevelUp -= IncreasePhase;
        TimeTickSystem.OnTick -= TimeTick;
    }
}

[System.Serializable]
public class SpawnPhase
{
    public int spawnCooldown;
    public string[] enemyTypes;
}
