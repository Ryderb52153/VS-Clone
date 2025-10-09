using UnityEngine;

public class LandMineAbility : Ability
{
    private bool isReady = true;
    private Camera myCamera;

    private void Awake()
    {
        myCamera = Camera.main;

        if (AbilityData == null) { return; }

        currentStats = AbilityData.GetBaseStats;
        nextLevelStats = AbilityData.GetBaseStats;
    }

    public override void Interact()
    {
        if (isReady)
        {
            UseAbility();
        }
    }

    protected override void OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        if (isReady) { return; }

        cooldownRemaining--;

        if (cooldownRemaining < 0)
        {
            isReady = true;
            InteractAvailable();
        }
    }


    protected override void UseAbility()
    {
        Vector3 mousePosition = myCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;


        //VacuumBehaviour vacuumBehaviour =
            //ObjectPooler.Instance.SpawnFromPool("Vacuum Ability", mousePosition, transform.rotation).GetComponent<VacuumBehaviour>();

        //vacuumBehaviour.SetVacuumDetails(currentStats);
        isReady = false;
        GameManager.Instance.ChangeCursor(CursorType.Default);
        PutAbilityOnCooldown();
    }
}
