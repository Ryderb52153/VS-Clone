using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    protected float cooldown = 50f;
    protected Vector3 direction;
    protected float speed;
    protected float damage;
    protected float lifeSpan;

    public float SetCooldown { set { cooldown = value; } }
    public Vector3 SetDirection { set { direction = value; } }
    public float SetSpeed { set { speed = value; } }
    public float SetDamage { set { damage = value; } }
    public float SetLifeSpan { set { lifeSpan = value; } }

    private float timeRemaining;

    public void StartProjectile()
    {
        timeRemaining = lifeSpan;
    }

    protected virtual void OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        timeRemaining--;

        if(timeRemaining < 0 )
        {
            timeRemaining = lifeSpan;
            this.gameObject.SetActive(false);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") { return; }

        if (collision.TryGetComponent<ItakeDamage>(out ItakeDamage hitCollider))
        {
            hitCollider.OnHit(damage);
            this.gameObject.SetActive(false);
        }
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

