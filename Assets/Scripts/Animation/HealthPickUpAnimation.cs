using UnityEngine;

public class HealthPickUpAnimation : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 20f;
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private SpriteRenderer spriteRenderer1 = null;
    [SerializeField] private SpriteRenderer spriteRenderer2 = null;

    private Vector3 moveDirection = Vector3.up;
    private float timer = 0f;
    private Color originalColor;
    private bool hasFaded = false;

    void Start()
    {
        originalColor = spriteRenderer1.color;
    }

    void Update()
    {
        if (hasFaded) { return; }

        transform.position += moveDirection * floatSpeed * Time.deltaTime;
        timer += Time.deltaTime;
        float fadeAmount = 1f - (timer / fadeDuration);

        Color newColor = originalColor;
        newColor.a = fadeAmount;
        spriteRenderer1.color = newColor;
        spriteRenderer2.color = newColor;

        if (fadeAmount <= 0f)
        {
            hasFaded = true;
            Transform parent = transform.parent;
            if (parent == null) { return; }
            FloatingHealthParent parentScript = parent.GetComponent<FloatingHealthParent>();
            parentScript.OnChildFadeComplete();
        }
    }

    public void Reset()
    {
        spriteRenderer1.color = originalColor;
        spriteRenderer2.color = originalColor;
        transform.localPosition = Vector3.zero;
        timer = 0;
        hasFaded = false;
    }
}
