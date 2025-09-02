using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreasureManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField, Min(1)] private int treasuresToSpawn = 3;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    [Header("Flow")]
    [Tooltip("Shuffle spawn points before spawning to add variety.")]
    [SerializeField] private bool shuffleSpawns = true;

    [Header("Events")]
    public UnityEvent onAllChestsOpened; // assign end-game behavior in Inspector

    [SerializeField] private FollowArrowUI followArrowUI = null;

    private readonly List<TreasureChest> chests = new();
    private int currentIndex = -1;

    public TreasureChest CurrentChest
    {
        get
        {
            if (currentIndex >= 0 && currentIndex < chests.Count)
                return chests[currentIndex];
            return null;
        }
    }

    private void Start()
    {
        InitializeAndBegin();
    }

    public void InitializeAndBegin()
    {
        chests.Clear();
        currentIndex = -1;

        if (spawnPoints.Count == 0)
        {
            Debug.LogError("TreasureManager: No spawn points assigned.");
            return;
        }

        // Optionally shuffle spawn order
        var points = new List<Transform>(spawnPoints);
        if (shuffleSpawns) FisherYates(points);

        SpawnChests(points);
        ActivateNext();
    }

    private void SpawnChests(List<Transform> points)
    {
        int count = treasuresToSpawn;

        for (int i = 0; i < count; i++)
        {
            var spawnLoc = points[i];

            var go = ObjectPooler.Instance.SpawnFromPool("Treasure Chest", spawnLoc.position, spawnLoc.rotation);
            go.transform.parent = spawnLoc.parent;
            var chest = go.GetComponent<TreasureChest>();
            if (!chest)
            {
                Debug.LogError("TreasureManager: Spawned prefab has no TreasureChest component.");
                Destroy(go);
                continue;
            }

            chest.gameObject.SetActive(false);
            chest.Opened += HandleChestOpened;
            chests.Add(chest);
        }
    }

    private void ActivateNext()
    {
        currentIndex++;
        if (currentIndex < chests.Count)
        {
            chests[currentIndex].gameObject.SetActive(true);
            followArrowUI.SetTarget = CurrentChest.transform;
        }
        else
        {
            onAllChestsOpened?.Invoke();
        }
    }

    private void HandleChestOpened(TreasureChest chest)
    {
        ActivateNext();
    }

    private static void FisherYates<T>(IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}