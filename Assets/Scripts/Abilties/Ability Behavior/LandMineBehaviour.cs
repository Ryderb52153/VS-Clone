using UnityEngine;

public class LandMineBehaviour : MonoBehaviour
{
    private bool isArmed;
    private float damage;
    private float cooldown;

    private void Awake()
    {
        isArmed = false;
    }

    public void SetLandMine(Stats stats)
    {
        isArmed = true;
        damage = stats.damage;
        cooldown = stats.cooldown;
    }

    private void OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        if (!isArmed) { return; }

        cooldown--;
        if (cooldown > 0) return;

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isArmed) { return; }

        if (collision.CompareTag("Enemy")) 
        {
            Explode();
            ObjectPooler.Instance.SpawnFromPool("Explosion FX", transform.position, transform.rotation);
        }
    }

    private void Explode()
    {
        isArmed = false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 5);

        foreach (Collider2D hit in hits)
        {
            if (!hit.CompareTag("Enemy")) { continue; }

            if (hit.TryGetComponent<ItakeDamage>(out ItakeDamage hitCollider))
            {
                hitCollider.OnHit(damage, transform.position);
            }
        }

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        TimeTickSystem.OnTick += OnTick;
    }

    private void OnDisable()
    {
        TimeTickSystem.OnTick -= OnTick;
    }
}