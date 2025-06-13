using UnityEngine;

public class ExperienceDrop : MonoBehaviour, IInteract
{
    public int ExperienceWorth { get; set; }

    public void Interact()
    {
        GameManager.Instance.AddExperience(ExperienceWorth);
        this.gameObject.SetActive(false);
    }
}
