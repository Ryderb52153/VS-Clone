using UnityEngine;

public class IceBoltProjectileBehaviour : ProjectileBehaviour
{
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") { return; }

        if (collision.TryGetComponent<ItakeDamage>(out ItakeDamage hitCollider))
        {
            hitCollider.OnHit(damage);
            GameObject gameObject = ObjectPooler.Instance.SpawnFromPool("Ice Cloud", transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }
}
