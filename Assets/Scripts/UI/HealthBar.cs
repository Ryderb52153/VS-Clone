using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar = null;
    [SerializeField] private TextMeshProUGUI healthAmountText = null;

    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = GameManager.Instance.Player.Stats;
        playerStats.HealthUpdate += HealthUpdate;
        SetInitialHealth(playerStats.MaxHealth);
    }

    private void SetInitialHealth(int maxHealth)
    {
        healthBar.value = maxHealth;
    }

    private void HealthUpdate(int maxHealth, int currentHealth)
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        float percent = (float)currentHealth / maxHealth;
        percent = Mathf.Round(percent * 100) / 100f;
        healthBar.value = percent;
        healthAmountText.text = $"{currentHealth}/{maxHealth}";
    }

    private void OnEnable()
    {
        if (playerStats != null)
            playerStats.HealthUpdate += HealthUpdate;
    }

    private void OnDisable()
    {
        playerStats.HealthUpdate -= HealthUpdate;
    }
}
