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
        playerStats.ExpUpdate += ScoreUpdate;
    }

    private void ScoreUpdate(int currentExp, int expToLevel)
    {
        currentScore += currentExp;
        scoreText.text = currentScore.ToString();
    }
}
