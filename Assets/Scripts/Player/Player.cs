using UnityEngine;
using UnityEngine.InputSystem;

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
        movement.MoveSpeed = Stats.MoveSpeed;
        combat.Stats = Stats;
    }


    //Input System Events

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        Movement.RawInputMovement = new Vector3(inputMovement.x, inputMovement.y, 0);
    }

    public void OnLeftClick(InputAction.CallbackContext value)
    {
        //have some kind of check if its enabled by an ability.
        //use the ability, send the location of the click if needed.
        //set a timer for the cool down.
        //print("Test left click");
    }
}
