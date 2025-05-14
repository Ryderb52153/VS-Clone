using UnityEngine;

public class ExperienceDrop : MonoBehaviour
{
    [SerializeField] private int ExperienceWorth = 1;

    public int GetExperienceWorth { get { return ExperienceWorth; } }

}
