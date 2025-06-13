using UnityEngine;

public class HealthUpDrop : MonoBehaviour, IInteract
{
    public int HealthAmount { get; set; }

    public void Interact()
    {
        GameManager.Instance.AddHealth(HealthAmount);
        this.gameObject.SetActive(false);
    }
}
