using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AbilityController : MonoBehaviour
{
    [SerializeField] private List<Ability> abilities;
    [SerializeField] private List<Ability> defaultAbilities;

    private void Start()
    {
        abilities[0].ActivateAbility();
    }

    public Ability GetStartingAbility => abilities[0];

    public Ability[] GetRandomAbilities(int numberOfAbilities)
    {
        System.Random random = new System.Random();

        // Get non-maxed abilities
        var available = abilities
            .Where(a => a.GetCurrentLevel < a.GetMaxLevel)
            .OrderBy(x => random.Next())
            .Take(numberOfAbilities)
            .ToList();

        // If not enough, fill the rest from defaultAbilities
        if (available.Count < numberOfAbilities)
        {
            int needed = numberOfAbilities - available.Count;

            var fallback = defaultAbilities
                .OrderBy(x => random.Next())
                .Take(needed);

            available.AddRange(fallback);
        }

        return available.ToArray();
    }
}

