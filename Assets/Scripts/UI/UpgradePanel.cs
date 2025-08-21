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
        abilityController.OnAbilityChanged += abilityRanksUI.SetRank;

        abilityRanksUI.SetRank(abilityController.GetStartingAbility);
    }

    private void ShowUpgradePanel()
    {
        upgradePanel.gameObject.SetActive(true);
        BindButtons();
    }

    private void BindButtons()
    {
        RemoveListenersFromButtons();

        var options = abilityController.GetUpgradeOptions(upgradeButtons.Length);

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            var btn = upgradeButtons[i];
            var nameLabel = buttonNameText[i];
            var descLabel = buttonTexts[i];

            var option = options[i];

            nameLabel.text = option.NextName;
            descLabel.text = option.NextDescription;

            btn.onClick.AddListener(() =>
            {
                // Perform the upgrade/activation
                option.Perform?.Invoke();

                // Rank UI will also update via OnAbilityChanged event, but this keeps it snappy
                abilityRanksUI.SetRank(option.Ability);

                ClosePanel();
            });
        }
    }

    private void ClosePanel()
    {
        upgradePanel.gameObject.SetActive(false);
        GameManager.Instance.UnpauseGame();
        RemoveListenersFromButtons();
    }

    private void RemoveListenersFromButtons()
    {
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].onClick.RemoveAllListeners();
        }
    }

    private void OnDestroy()
    {
        if (playerstats != null) playerstats.LevelUp -= ShowUpgradePanel;
        if (abilityController != null) abilityController.OnAbilityChanged -= abilityRanksUI.SetRank;
    }
}