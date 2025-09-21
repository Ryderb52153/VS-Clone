using UnityEngine;

public class DefenseBallBehaviour : ProjectileBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameManager.Instance.Player.gameObject;
    }

    private void FixedUpdate()
    {
        transform.RotateAround(player.transform.localPosition, Vector3.forward , speed);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") { return; }

        if (collision.TryGetComponent<ItakeDamage>(out ItakeDamage hitCollider))
        {
            hitCollider.OnHit(damage, transform.position);
        }
    }
}
