using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel = null;
    [SerializeField] private Button[] upgradeButtons = null;
    [SerializeField] private AbilityRanksUI abilityRanksUI = null;
    [SerializeField] private TextMeshProUGUI[] buttonNameText;
    [SerializeField] private TextMeshProUGUI[] buttonTexts = null;

    private PlayerStats playerstats;
    private AbilityController abilityController;

    private void Start()
    {
        playerstats = GameManager.Instance.Player.Stats;
        abilityController = GameManager.Instance.Player.AbilityController;
        playerstats.LevelUp += ShowUpgradePanel;
    }

    private void ShowUpgradePanel()
    {
        upgradePanel.gameObject.SetActive(true);
        SetAbilitiesToButtons();
    }

    private void SetAbilitiesToButtons()
    {
        Ability[] randomAbilities = abilityController.GetRandomAbilities(upgradeButtons.Length);

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            if (randomAbilities[i].IsActive)
            {
                SetButtonForAbilityLevelUp(randomAbilities, i);
            }
            else
            {
                SetButtonForAbilityActivate(randomAbilities, i);
            }

            // Ability Ranks UI 
            int numb = i;

            upgradeButtons[i].onClick.AddListener(
                () => abilityRanksUI.SetNewRank(randomAbilities[numb]));

            // Ability Ranks UI 

            upgradeButtons[i].onClick.AddListener(ButtonPressed);
        }
    }

    private void SetButtonForAbilityActivate(Ability[] randomAbilities, int i)
    {
        upgradeButtons[i].onClick.AddListener(randomAbilities[i].ActivateAbility);
        buttonTexts[i].text = randomAbilities[i].GetNextLevelDescriptionText;
        buttonNameText[i].text = randomAbilities[i].GetNextLevelNameText;
    }

    private void SetButtonForAbilityLevelUp(Ability[] randomAbilities, int i)
    {
        upgradeButtons[i].onClick.AddListener(randomAbilities[i].LevelUpAbililty);
        buttonTexts[i].text = randomAbilities[i].GetNextLevelDescriptionText;
        buttonNameText[i].text = randomAbilities[i].GetNextLevelNameText;
    }

    private void ButtonPressed()
    {
        upgradePanel.gameObject.SetActive(false);
        GameManager.Instance.UnpauseGame();
        RemoveListenersFromButtons();
    }

    private void RemoveListenersFromButtons()
    {
        for (int i = 0; i < 3; i++)
        {
            upgradeButtons[i].onClick.RemoveAllListeners();
        }
    }
}
