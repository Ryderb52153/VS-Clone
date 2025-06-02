using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar = null;
    [SerializeField] private TextMeshProUGUI healthAmountText = null;

    PlayerStats playerStats;

    private void Start()
    {
        playerStats = GameManager.Instance.Player.Stats;
        playerStats.HealthUpdate += HealthUpdate;
        SetInitialHealth(playerStats.MaxHealth);
    }

    private void SetInitialHealth(int maxHealth)
    {
        healthBar.value = maxHealth;
        print("Health Bar Initial :" + healthBar.value);
    }

    private void HealthUpdate(int maxHealth, int currentHealth)
    {
        float healthPercent = (float)currentHealth / (float)maxHealth;
        healthBar.value = healthPercent;
        healthAmountText.text = currentHealth + "/" + maxHealth;
        //print("Health Bar Updated :" + healthBar.value);
    }

    public void HealthBarChanged()
    {
        print("Health Bar changed " + healthBar.value);
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
