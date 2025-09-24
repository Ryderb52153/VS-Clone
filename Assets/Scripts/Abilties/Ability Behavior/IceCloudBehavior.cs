using System;
using UnityEngine;

public class IceCloudBehavior : MonoBehaviour
{
    private float timeRemaining;
    private float lifeSpan = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") { return; }

        if (collision.TryGetComponent<ItakeDamage>(out ItakeDamage hitCollider))
        {
            hitCollider.Slow(1, 1f);
        }
    }

    private void OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        timeRemaining--;

        if (timeRemaining < 0)
        {
            timeRemaining = lifeSpan;
            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        TimeTickSystem.OnTick += OnTick;
        timeRemaining = lifeSpan;
    }

    private void OnDisable()
    {
        TimeTickSystem.OnTick -= OnTick;
    }
}