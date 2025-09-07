using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private PlayerStats playerStats;

    private int currentScore = 0;

    private void Start()
    {
        playerStats = GameManager.Instance.Player.Stats;
        playerStats.PointUpdate += ScoreUpdate;
        GameManager.Instance.gameOverDelegate += RecordPlayerScore;
    }

    private void RecordPlayerScore()
    {
        int currentSavedScore = PlayerPrefs.GetInt("TotalScore", 0);
        PlayerPrefs.SetInt("TotalScore", currentSavedScore + currentScore);
        PlayerPrefs.Save();
    }

    private void ScoreUpdate(int points)
    {
        currentScore += points;
        scoreText.text = currentScore.ToString();
    }
}