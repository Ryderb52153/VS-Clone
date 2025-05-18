using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player = null;

    public static GameManager Instance;

    public delegate void EndGameDelegate();
    public delegate void GameOverDelegate();
    public Player Player { get { return player; } }
    public EndGameDelegate endGameDelegate;
    public GameOverDelegate gameOverDelegate;

    public List<GameObject> ActiveEnemies { get;  set; }

    private void Awake()
    {
        UnpauseGame();

        ActiveEnemies = new List<GameObject>();

        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    public void TriggerEndGame()
    {
        endGameDelegate.Invoke();
    }

    public void TriggerGameOver()
    {
        PauseGame();
        gameOverDelegate.Invoke();
    }

    private void OnEnable()
    {
        if (Player != null)
            Player.Stats.LevelUp += PauseGame;
    }

    private void OnDisable()
    {
        if(Player != null)
            Player.Stats.LevelUp -= PauseGame;
    }
}
