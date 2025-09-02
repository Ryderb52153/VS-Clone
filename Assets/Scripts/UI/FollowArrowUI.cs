using UnityEngine;

public class FollowArrowUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform arrowUI; 
    [SerializeField] private Transform player;     

    private Transform target;

    public Transform SetTarget { set { target = value; } }

    private void Update()
    {
        if (target == null) { return; }

        Vector3 v = target.position - player.position;
        arrowUI.eulerAngles = new Vector3(0, 0, Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg);
    }
}