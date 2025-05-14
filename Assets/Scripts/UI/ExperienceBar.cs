using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] private Slider expBar = null;

    PlayerStats playerStats;

    private void Start()
    {
        playerStats = GameManager.Instance.Player.Stats;
        playerStats.ExpUpdate += ExpUpdate;
    }

    private void ExpUpdate(int exp)
    {
        expBar.value = exp *.01f;
    }

    private void OnEnable()
    {
        if (playerStats != null)
            playerStats.ExpUpdate += ExpUpdate;
    }

    private void OnDisable()
    {
        playerStats.ExpUpdate -= ExpUpdate;
    }
}
