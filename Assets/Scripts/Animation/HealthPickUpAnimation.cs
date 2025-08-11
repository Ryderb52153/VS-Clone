using UnityEngine;

public class HealthPickUpAnimation : MonoBehaviour
{
    public void AnimationEnded()
    {
        this.gameObject.SetActive(false);
    }
}
