using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] private Sprite chestCloseSprite;
    [SerializeField] private Sprite chestOpenSprite;
    [SerializeField] private ParticleSystem openSparkleEffects;

    private SpriteRenderer spriteRenderer;
    private bool isopen = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isopen = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isopen) { return; }

        if (collision.transform.tag == "Player")
        {
            isopen = true;
            openSparkleEffects.Play();
            spriteRenderer.sprite = chestOpenSprite;
        }
    }
}