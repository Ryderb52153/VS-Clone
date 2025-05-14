using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyStats stats = null;

    private GameObject player;

    private void Awake()
    {
        player = GameManager.Instance.Player.gameObject;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, stats.EnemySpeed * Time.deltaTime);
    }
}
