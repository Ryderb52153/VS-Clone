using UnityEngine;

public class CompassArrow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform arrowUI; // your UI Image/RectTransform
    [SerializeField] private Transform player;      // player/world origin
    [SerializeField] private Transform target;      // destination (e.g., chest)


    private void Update()
    {
        Vector3 v = target.position - player.position;
        arrowUI.eulerAngles = new Vector3(0, 0, Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg);
    }
}