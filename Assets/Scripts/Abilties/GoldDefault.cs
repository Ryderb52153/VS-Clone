using UnityEngine;

public class GoldDefault : Ability
{
    public override void ActivateAbility()
    {
        base.ActivateAbility();
        print("Add 10 gold");
    }

    public override void LevelUpAbililty()
    {
        base.LevelUpAbililty();
        print("add 10 gold");
    }
}
