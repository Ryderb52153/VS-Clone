using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int MoveSpeed { set; private get; }
    public Vector2 RawInputMovement { set; private get; }

    private void FixedUpdate()
    {
        transform.Translate(RawInputMovement * MoveSpeed * Time.deltaTime);
    }
}
