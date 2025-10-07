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

    private Ability leftClickAbility;

    private void Awake()
    {
        movement.MoveSpeed = Stats.MoveSpeed;
        combat.Stats = Stats;
        abilityController.GetAbilityInteracts.AbilityAddedToQue += CheckLeftClick;
    }

    //Input System Events
    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        Movement.RawInputMovement = new Vector3(inputMovement.x, inputMovement.y, 0);
    }

    public void OnLeftClick(InputAction.CallbackContext value)
    {
        if (GameManager.Instance.isPaused) { return; }
        if (!value.performed) { return; }
        if (leftClickAbility == null) { return; }

        leftClickAbility.Interact();
        leftClickAbility = null;
        CheckLeftClick();
    }

    public void OnDash(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (GameManager.Instance.isPaused) return;

        Vector2 mouseScreen = Mouse.current.position.ReadValue();
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        movement.TryDashTowards(mouseWorld);
    }

    public void CheckLeftClick()
    {
        if (leftClickAbility == null)
        {
            leftClickAbility = abilityController.GetAbilityInteracts.GetNextAbilityInQue();

            if (leftClickAbility != null)
            {
                GameManager.Instance.ChangeCursor(leftClickAbility.GetCursor);
            }
        }
    }
}

public enum InputKey
{
    None,
    Leftclick,
    Rightclick
}