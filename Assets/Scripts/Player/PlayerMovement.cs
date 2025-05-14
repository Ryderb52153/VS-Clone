using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStats Stats { set; private get; }
    private Vector2 rawInputMovement;

    private void FixedUpdate()
    {
        transform.Translate(rawInputMovement * Stats.MoveSpeed * Time.deltaTime);
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, inputMovement.y, 0);
    }
}
