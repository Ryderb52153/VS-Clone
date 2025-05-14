using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AbilityController : MonoBehaviour
{
    [SerializeField] private List<Ability> abilities;

    private void Start()
    {
        abilities[0].ActivateAbility();
    }

    public Ability[] GetRandomAbilities(int numberOfAbilities)
    {
        List<Ability> randomAbilities = new List<Ability>();
        System.Random random = new System.Random();

        return abilities.OrderBy(x => random.Next()).Take(numberOfAbilities).ToArray();
    }
}

