using UnityEngine;

public class RankBoxes : MonoBehaviour
{
    public void SetRankAmount(int rank)
    {
        int totalChildren = transform.childCount;
        int activateCount = Mathf.Min(rank, totalChildren);

        for (int i = 0; i < totalChildren; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i < activateCount);
        }
    }
}
