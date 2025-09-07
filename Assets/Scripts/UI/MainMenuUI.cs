using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI openChestsText;
    [SerializeField] private TextMeshProUGUI enemiesKilledText;

    private void Start()
    {
        SetPlayerStats();
    }

    private void SetPlayerStats()
    {
        int totalScore = PlayerPrefs.GetInt("TotalScore", 0);
        scoreText.text = "Total Score: " + totalScore;

        int totalChests = PlayerPrefs.GetInt("ChestsOpened", 0);
        openChestsText.text = "Total Chests Opened : " + totalChests;

        int totalKills = PlayerPrefs.GetInt("EnemiesKilled", 0);
        enemiesKilledText.text = "Total Enemies Killed : " + totalKills;
    }

    public void StartLevelButton()
    {
        SceneManager.LoadScene("Game_Scene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("TotalScore");
        PlayerPrefs.DeleteKey("ChestsOpened");
        PlayerPrefs.DeleteKey("EnemiesKilled");
        PlayerPrefs.Save();

        SetPlayerStats();
    }
}
