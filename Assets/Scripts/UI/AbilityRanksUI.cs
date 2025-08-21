using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityRanksUI : MonoBehaviour
{
    [SerializeField] private Image[] rankImages = null;
    [SerializeField] private TextMeshProUGUI[] rankTexts = null;
    [SerializeField] private Image[] cooldownOverlays = null;
    [SerializeField] private TextMeshProUGUI[] cooldownTexts = null;

    private Dictionary<Ability, UIData> abilityUIMap = new Dictionary<Ability, UIData>();
    private int nextAvailableIndex = 0;

    private struct UIData
    {
        public Image image;
        public TextMeshProUGUI rankText;
        public Image cooldownOverlay;
        public TextMeshProUGUI countdownText;
        public Coroutine cooldownCo;
    }

    public void SetRank(Ability ability)
    {
        UIData ui;

        if (!abilityUIMap.ContainsKey(ability))
            ui = RegisterAbilityUI(ability);
        else
            ui = abilityUIMap[ability];

        ui.image.sprite = ability.GetSprite;
        ui.rankText.text = ability.GetCurrentLevel.ToString();
    }

    public void TriggerCooldown(Ability ability, float durationSeconds)
    {
        if (durationSeconds <= 0f) return;

        UIData ui = abilityUIMap[ability];

        if (ui.cooldownCo != null)
            StopCoroutine(ui.cooldownCo);

        ui.cooldownCo = StartCoroutine(CooldownRoutine(ui, durationSeconds - .01f));
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
            rankText = rankTexts[nextAvailableIndex],
            cooldownOverlay = cooldownOverlays[nextAvailableIndex],
            countdownText = cooldownTexts[nextAvailableIndex]
        };

        abilityUIMap[ability] = data;
        abilityUIMap[ability].rankText.gameObject.SetActive(true);
        abilityUIMap[ability].image.gameObject.SetActive(true);
        ability.OnAbilityUsed += TriggerCooldown;
        nextAvailableIndex++;
        return data;
    }

    private IEnumerator CooldownRoutine(UIData ui, float durationSeconds)
    {
        if (!ui.cooldownOverlay) yield break;

        ui.cooldownOverlay.enabled = true;
        ui.cooldownOverlay.fillAmount = 1f;
        if (ui.countdownText) ui.countdownText.enabled = true;

        float timeDuration = durationSeconds;

        while (timeDuration > 0f)
        {
            float dt = Time.deltaTime;
            timeDuration -= dt;

            float pct = Mathf.Clamp01(timeDuration / durationSeconds);
            ui.cooldownOverlay.fillAmount = pct;

            if (ui.countdownText)
            {
                int secondsLeft = Mathf.CeilToInt(timeDuration);
                ui.countdownText.text = secondsLeft > 0 ? secondsLeft.ToString() : "";
            }

            yield return null;
        }

        // Done
        ui.cooldownOverlay.fillAmount = 0f;
        ui.cooldownOverlay.enabled = false;
        if (ui.countdownText) { ui.countdownText.text = ""; ui.countdownText.enabled = false; }
        ui.cooldownCo = null;
    }
}