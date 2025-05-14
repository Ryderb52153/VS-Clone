using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel = null;
    [SerializeField] private Button resetButton = null;
    
    private void Start()
    {
        resetButton.onClick.AddListener(ResetScene);
        GameManager.Instance.gameOverDelegate = ShowGameOverPanel;
    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.gameObject.SetActive(true);
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
