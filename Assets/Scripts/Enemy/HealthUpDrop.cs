using UnityEngine;
using UnityEngine.EventSystems;

public class HealthUpDrop : MonoBehaviour, IInteract, IPointerClickHandler
{
    public int HealthAmount { get; set; }

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
}
