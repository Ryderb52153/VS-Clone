using UnityEngine;

public class TankUp : Ability
{
    protected override void ActivatePassive()
    {
        base.ActivatePassive();

        PlayerStats playerStats = GameManager.Instance.Player.Stats;

        playerStats.AddMaxHealth(10);
    }

    protected override void DeactivatePassive()
    {
        base.DeactivatePassive();

        PlayerStats playerStats = GameManager.Instance.Player.Stats;
        playerStats.AddMaxHealth(-10);
    }
}
