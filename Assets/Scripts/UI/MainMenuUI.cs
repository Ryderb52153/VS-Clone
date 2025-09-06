using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void StartLevelButton()
    {
        SceneManager.LoadScene("Game_Scene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
