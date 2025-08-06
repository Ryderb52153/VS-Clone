using UnityEngine;
using UnityEngine.EventSystems;

public class HealthUpDrop : MonoBehaviour, IInteract, IPointerClickHandler
{
    [SerializeField] private GameObject circleShader = null;
    [SerializeField] private  float rotationSpeed = 30f;

    private Material circleMaterial;
    private float rotationValue = 0f;

    public int HealthAmount { get; set; }

    private void Awake()
    {
        circleMaterial = circleShader.GetComponent<SpriteRenderer>().material;
    }

    public void Interact()
    {
        ObjectPooler.Instance.SpawnFromPool("Health Animation", transform.position, Quaternion.identity);
        GameManager.Instance.AddHealth(HealthAmount);
        this.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Interact();
    }

    private void Update()
    {
        if (circleMaterial == null) { return; }

        rotationValue += rotationSpeed * Time.deltaTime;

        if (rotationValue > 360f) rotationValue -= 360f;

        circleMaterial.SetFloat("_Rotation", rotationValue);
    }
}