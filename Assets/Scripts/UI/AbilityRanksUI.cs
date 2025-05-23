using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityRanksUI : MonoBehaviour
{
    [SerializeField] private Image[] rankImages = null;
    [SerializeField] private TextMeshProUGUI[] rankTexts = null;

    

    public void SetNewRank(Ability ability)
    {
        print(ability.GetNextLevelNameText + " is new rank : " + ability.GetCurrentLevel);
    }
}
