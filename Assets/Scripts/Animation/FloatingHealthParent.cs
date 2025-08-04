using UnityEngine;

public class FloatingHealthParent : MonoBehaviour
{
    [SerializeField] private HealthPickUpAnimation[] childObjects;

    private int counter = 0;

    public void OnChildFadeComplete()
    {
        counter++;

        if (counter >= childObjects.Length)
        {
            counter = 0;
            ResetChildren();
            gameObject.SetActive(false);
        }
    }

    private void ResetChildren()
    {
        foreach (var child in childObjects)
        {
            child.Reset();
        }
    }
}
