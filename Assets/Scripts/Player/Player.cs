using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats stats = null;
    [SerializeField] private PlayerMovement movement = null;
    [SerializeField] private PlayerCombat combat = null;
    [SerializeField] private AbilityController abilityController = null;

    public PlayerStats Stats { get { return stats; } }
    public PlayerMovement Movement { get { return movement; } }
    public PlayerCombat Combat { get { return combat; } }
    public AbilityController AbilityController { get { return abilityController; } }

    private void Awake()
    {
        movement.Stats = Stats;
        combat.Stats = Stats;
    }
}
