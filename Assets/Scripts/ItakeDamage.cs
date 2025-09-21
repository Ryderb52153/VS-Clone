using UnityEngine;

public interface ItakeDamage
{
    void OnHit(float damage);
    void OnHit(float damage, Vector3 sourceWorldPos, float knockbackForce = 3.5f);
    void Slow(int slowAmount, float duration);
}