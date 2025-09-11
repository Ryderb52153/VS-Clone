using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashDistance = 5f;      
    [SerializeField] private float dashDuration = 0.12f;   
    [SerializeField] private float dashCooldown = 0.5f;    
    [SerializeField] private LayerMask dashBlockers;     

    private bool isDashing = false;
    private float nextDashTime = 0f;

    public int MoveSpeed { set; private get; }
    public Vector2 RawInputMovement { set; private get; }

    private void FixedUpdate()
    {
        if (isDashing) return;
        transform.Translate(RawInputMovement * MoveSpeed * Time.deltaTime);
    }

    public void TryDashTowards(Vector2 worldTarget)
    {
        if (Time.time < nextDashTime || isDashing) return;

        Vector2 pos = transform.position;
        Vector2 dir = (worldTarget - pos).normalized;

        // Stop dash early if a wall is in the way
        float maxDist = dashDistance;
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, dashDistance, dashBlockers);
        if (hit.collider != null)
        {
            // leave a small buffer so we don't overlap the wall
            maxDist = Mathf.Max(0f, hit.distance - 0.05f);
        }

        StartCoroutine(DashRoutine(dir, maxDist));
    }

    private IEnumerator DashRoutine(Vector2 dir, float dist)
    {
        isDashing = true;
        Vector3 start = transform.position;
        Vector3 end = start + (Vector3)(dir * dist);

        float timer = 0f;
        while (timer < dashDuration)
        {
            timer += Time.deltaTime;
            float a = Mathf.Clamp01(timer / dashDuration);
            // simple linear dash; swap for SmoothStep if you want easing
            transform.position = Vector3.Lerp(start, end, a);
            yield return null;
        }

        transform.position = end;
        isDashing = false;
        nextDashTime = Time.time + dashCooldown;
    }
}