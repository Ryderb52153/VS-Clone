using UnityEngine;
using UnityEngine.UI;

public class AbilityRanksUI : MonoBehaviour
{
    [SerializeField] private Image[] rankImages = null;

    

    public void SetNewRank(string abilityName, int currentLevel)
    {
        print(abilityName + " is new rank : " + currentLevel);
    }

    public void SetExistingRank(string abilityName, int currentLevel)
    {
        print(abilityName + " is new rank : " + currentLevel);
    }
}
