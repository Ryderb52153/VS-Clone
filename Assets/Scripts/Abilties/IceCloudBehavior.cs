using UnityEngine;

public class IceCloudBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") { return; }

        if (collision.TryGetComponent<ItakeDamage>(out ItakeDamage hitCollider))
        {
            hitCollider.Slow(1, 2f);
            gameObject.SetActive(false);
        }
    }
}