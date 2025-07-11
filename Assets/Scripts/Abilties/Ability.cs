using System.Collections;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected AbilityData AbilityData;

    private float cooldownRemaining;
    protected int currentLevel = 1;
    protected Stats currentStats;
    protected Stats nextLevelStats;

    public string GetNextLevelDescriptionText { get => nextLevelStats.descriptionText; }
    public string GetNextLevelNameText { get => nextLevelStats.nameText; }
    public int GetCurrentLevel { get => currentLevel; }
    public int GetMaxLevel { get => AbilityData.GetMaxLevel; }
    public Sprite GetSprite { get => AbilityData.GetSprite; }
    public bool IsActive = false;

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

    protected virtual void OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        cooldownRemaining--;

        if (cooldownRemaining < 0)
        {
            if (currentStats.attackInterval > 1)
                StartCoroutine(AttackIntervalCoroutine());
            else if (currentStats.attackInterval == 1)
                Attack();
        }
    }

    IEnumerator AttackIntervalCoroutine()
    {
        for (int i = 0; i < currentStats.attackInterval; i++)
        {
            Attack();
            yield return new WaitForSeconds(.1f);
        }
    }

    protected virtual void Attack()
    {
        cooldownRemaining = currentStats.cooldown;
    }

    private void OnDestroy()
    {
        DeactivateAbility();
    }
}
