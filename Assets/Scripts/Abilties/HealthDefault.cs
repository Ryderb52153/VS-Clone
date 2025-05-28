using UnityEngine;

public class HealthDefault : Ability
{
    protected override void ActivatePassive()
    {
        base.ActivatePassive();
        PlayerStats playerStats = GameManager.Instance.Player.Stats;
        playerStats.AddMaxHealth(currentStats.healingAmount);
    }

    protected override void DeactivatePassive()
    {
        base.DeactivatePassive();

        PlayerStats playerStats = GameManager.Instance.Player.Stats;
        playerStats.AddMaxHealth(-currentStats.healingAmount);
    }
}
