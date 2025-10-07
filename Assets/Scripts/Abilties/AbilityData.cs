using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "Scriptable Objects/AbilityData")]
public class AbilityData : ScriptableObject
{
    [SerializeField] private Sprite abilitySprite;
    [SerializeField] private int maxLevel = 1;
    [SerializeField] private Stats baseStats;
    [SerializeField] private Stats[] LevelUpStats;

    public Stats GetBaseStats => baseStats;
    public int GetMaxLevel { get { return maxLevel; } } 
    public Sprite GetSprite { get { return abilitySprite; } }
    public CursorType GetCursorType { get { return baseStats.cursor; } }

    public Stats GetLevelData(int level)
    {
        if(level - 2 <  LevelUpStats.Length)
        {
            return LevelUpStats[level - 2];
        }

        Debug.LogWarning("Ability data not set up to " + level);
        return new Stats();
    }
}


[Serializable]
public struct Stats
{
    public string nameText;
    public string descriptionText;

    public float moveSpeed;
    public float cooldown;
    public float damage;
    public float damageVariance;
    public float lifeSpan;
    public float attackInterval;
    public int healingAmount;
    public bool isPassive;
    public bool isInputInteractable;
    public CursorType cursor;


    public static Stats operator +(Stats statsOne, Stats otherStats)
    {
        Stats baseStats = new Stats();

        baseStats.nameText = otherStats.nameText ?? statsOne.nameText;
        baseStats.descriptionText = otherStats.descriptionText ?? statsOne.descriptionText;

        baseStats.moveSpeed = statsOne.moveSpeed + otherStats.moveSpeed;
        baseStats.cooldown = statsOne.cooldown + otherStats.cooldown;
        baseStats.damage = statsOne.damage + otherStats.damage;
        baseStats.damageVariance = statsOne.damageVariance + otherStats.damageVariance;
        baseStats.lifeSpan = statsOne.lifeSpan + otherStats.lifeSpan;
        baseStats.attackInterval = statsOne.attackInterval + otherStats.attackInterval;
        baseStats.healingAmount = statsOne.healingAmount + otherStats.healingAmount;
        baseStats.isPassive = statsOne.isPassive;
        baseStats.isInputInteractable = statsOne.isInputInteractable;

        return baseStats;
    }

    public float GetDamage()
    {
        return damage + UnityEngine.Random.Range(0, damageVariance);
    }
}

