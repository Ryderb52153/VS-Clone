using UnityEngine;

public class FollowArrowUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform arrowUI; 
    [SerializeField] private Transform player;     
    [SerializeField] private Transform target;      

    private void Update()
    {
        Vector3 v = target.position - player.position;
        arrowUI.eulerAngles = new Vector3(0, 0, Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg);
    }
}