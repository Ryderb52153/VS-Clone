using System;
using System.Collections;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected AbilityData AbilityData;

    protected float cooldownRemaining;
    protected int currentLevel = 1;
    protected Stats currentStats;
    protected Stats nextLevelStats;

    public string GetNextLevelDescriptionText { get => nextLevelStats.descriptionText; }
    public string GetNextLevelNameText { get => nextLevelStats.nameText; }
    public int GetCurrentLevel { get => currentLevel; }
    public int GetMaxLevel { get => AbilityData.GetMaxLevel; }
    public Sprite GetSprite { get => AbilityData.GetSprite; }
    public CursorType GetCursor { get => AbilityData.GetCursorType; }
    public bool IsActive { get; set; } = false;
    public bool IsInputInteractable { get => currentStats.isInputInteractable; }
    public event Action<Ability, float> OnAbilityUsed;
    public event Action<Ability> InteractReady;

    private void Awake()
    {
        if (AbilityData == null) { return; }

        currentStats = AbilityData.GetBaseStats;
        nextLevelStats = AbilityData.GetBaseStats;
    }

    public virtual void ActivateAbility()
    {
        nextLevelStats += AbilityData.GetLevelData(currentLevel + 1);
        IsActive = true;

        if (!currentStats.isPassive)
            TimeTickSystem.OnTick += OnTick;
        else
            ActivatePassive();
    }

    public virtual void LevelUpAbililty()
    {
        if(!CanLevelUP()) { return; }

        currentStats += AbilityData.GetLevelData(++currentLevel);

        if (CanLevelUP())
            nextLevelStats += AbilityData.GetLevelData(currentLevel + 1);

        if (currentStats.isPassive)
            ActivatePassive();
    }

    public virtual bool CanLevelUP()
    {
        return currentLevel < AbilityData.GetMaxLevel;
    }


    public virtual void DeactivateAbility()
    {
        IsActive = false;

        if(!currentStats.isPassive)
            TimeTickSystem.OnTick -= OnTick;
        else
            DeactivatePassive();
    }

    protected virtual void ActivatePassive()
    {

    }

    protected virtual void DeactivatePassive()
    {

    }

    public virtual void Interact()
    {
        
    }

    protected virtual void OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        cooldownRemaining--;
        if (cooldownRemaining > 0) return;

        if (currentStats.attackInterval > 1)
            StartCoroutine(AttackIntervalCoroutine());
        else if (currentStats.attackInterval == 1)
            UseAbility();

        PutAbilityOnCooldown();
    }

    protected void PutAbilityOnCooldown()
    {
        cooldownRemaining = currentStats.cooldown;
        OnAbilityUsed.Invoke(this, currentStats.cooldown);
    }

    IEnumerator AttackIntervalCoroutine()
    {
        for (int i = 0; i < currentStats.attackInterval; i++)
        {
            UseAbility();
            yield return new WaitForSeconds(.1f);
        }
    }

    protected virtual void UseAbility()
    {

    }

    protected virtual void InteractAvailable()
    {
        InteractReady.Invoke(this);
    }

    private void OnDestroy()
    {
        DeactivateAbility();
    }
}