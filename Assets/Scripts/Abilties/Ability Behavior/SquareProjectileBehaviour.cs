using UnityEngine;

public class SquareProjectileBehaviour : ProjectileBehaviour
{
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
