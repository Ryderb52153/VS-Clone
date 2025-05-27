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

    private void ExpUpdate(int currentExp, int expToLevel)
    {
        float expPerecent = (float)currentExp / expToLevel;
        expBar.value = expPerecent;
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
