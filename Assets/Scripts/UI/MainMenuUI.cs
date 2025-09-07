using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        int totalScore = PlayerPrefs.GetInt("TotalScore", 0);
        scoreText.text = "Total Score: " + totalScore;
    }

    public void StartLevelButton()
    {
        SceneManager.LoadScene("Game_Scene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
