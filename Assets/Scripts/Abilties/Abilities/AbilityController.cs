using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [SerializeField] private List<Ability> abilities;
    [SerializeField] private List<Ability> defaultAbilities;
    [SerializeField] private AbilityInteracts interacts;

    public Ability GetStartingAbility => abilities[0];
    public AbilityInteracts GetAbilityInteracts => interacts;
    public event Action<Ability> OnAbilityChanged;

    private System.Random rng = new System.Random();

    private void Start()
    {
        if (abilities == null || abilities.Count <= 0) { return; }
        ActivateAbility(abilities[0]);
    }

    public UpgradeOption[] GetUpgradeOptions(int count)
    {
        // Shuffle helper
        IEnumerable<T> Shuffle<T>(IEnumerable<T> src) => src.OrderBy(_ => rng.Next());

        var pool = abilities
            .Where(a => a.GetCurrentLevel < a.GetMaxLevel)
            .ToList();

        // Not enough? pad with defaults
        if (pool.Count < count)
        {
            var needed = count - pool.Count;
            pool.AddRange(Shuffle(defaultAbilities).Take(needed));
        }

        // Take random selection and build options
        return Shuffle(pool)
            .Take(count)
            .Select(a => BuildOption(a))
            .ToArray();
    }

    private UpgradeOption BuildOption(Ability ability)
    {
        bool isActive = ability.IsActive;

        return new UpgradeOption
        {
            Ability = ability,
            NextName = ability.GetNextLevelNameText,
            NextDescription = ability.GetNextLevelDescriptionText,
            IsActivation = !isActive,
            Perform = () =>
            {
                if (!isActive)
                    ActivateAbility(ability);
                else
                    LevelUpAbility(ability);
            }
        };
    }

    public void ActivateAbility(Ability ability)
    {
        if (ability.IsActive) { return; }

        ability.ActivateAbility();
        OnAbilityChanged?.Invoke(ability);

        if (ability.IsInputInteractable)
        {
            interacts.AddAbility(ability);
        }
    }

    public void LevelUpAbility(Ability ability)
    {
        if (ability.GetCurrentLevel < ability.GetMaxLevel)
        {
            ability.LevelUpAbililty();
            OnAbilityChanged?.Invoke(ability);
        }
    }

    public struct UpgradeOption
    {
        public Ability Ability;
        public string NextName;
        public string NextDescription;
        public Action Perform;
        public bool IsActivation;
    }
}