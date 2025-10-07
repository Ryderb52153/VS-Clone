using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInteracts : MonoBehaviour
{
    public Queue<Ability> QueueOfActivatableAbilities = new Queue<Ability>();

    public event Action AbilityAddedToQue;

    public Ability GetNextAbilityInQue()
    { 
        if(QueueOfActivatableAbilities.Count <= 0)
            return null;

        return QueueOfActivatableAbilities.Dequeue();
    }

    public void AddAbility(Ability ability)
    {
        AddToQue(ability);
        ability.InteractReady += AddToQue;
    }

    public void AddToQue(Ability ability)
    {
        QueueOfActivatableAbilities.Enqueue(ability);
        AbilityAddedToQue.Invoke();
    }
}
