using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText = null;
    [SerializeField] private float endGameTimer = 60.0f;

    private float currentTime;
    private bool endGame = false;

    private void Update()
    {
        if (endGame) { return; }

        currentTime += Time.deltaTime;
        DisplayTime(currentTime);

        if(currentTime >= endGameTimer)
        {
            GameManager.Instance.TriggerEndGame();
            endGame = true;   
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
