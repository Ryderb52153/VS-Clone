using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityRanksUI : MonoBehaviour
{
    [SerializeField] private Image[] rankImages = null;
    [SerializeField] private TextMeshProUGUI[] rankTexts = null;

    private Dictionary<Ability, UIData> abilityUIMap = new Dictionary<Ability, UIData>();
    private int nextAvailableIndex = 0;

    private struct UIData
    {
        public Image image;
        public TextMeshProUGUI text;
    }

    public void SetRank(Ability ability)
    {
        UIData ui;

        if (!abilityUIMap.ContainsKey(ability))
            ui = RegisterAbilityUI(ability);
        else
            ui = abilityUIMap[ability];

        ui.image.sprite = ability.GetSprite;
        ui.text.text = ability.GetCurrentLevel.ToString();
    }


    private UIData RegisterAbilityUI(Ability ability)
    {
        if (nextAvailableIndex >= rankImages.Length || nextAvailableIndex >= rankTexts.Length)
        {
            Debug.LogError("No more available UI slots for abilities.");
            return new UIData();
        }

        UIData data = new UIData
        {
            image = rankImages[nextAvailableIndex],
            text = rankTexts[nextAvailableIndex]
        };

        abilityUIMap[ability] = data;
        abilityUIMap[ability].text.gameObject.SetActive(true);
        abilityUIMap[ability].image.gameObject.SetActive(true);
        nextAvailableIndex++;
        return data;
    }

}
