using UnityEngine;

public class VacuumPU : Ability
{
    private bool isReady = false;
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
        if(isReady)
        {
            Attack();
        }
    }

    protected override void OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        if (isReady) { return; }

        cooldownRemaining--;

        if (cooldownRemaining < 0)
        {
            isReady = true;
        }
    }

    protected override void Attack()
    {
        base.Attack();

        Vector3 mousePosition = myCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        VacuumBehaviour vacuumBehaviour = 
            ObjectPooler.Instance.SpawnFromPool("Vacuum Ability", mousePosition, transform.rotation).GetComponent<VacuumBehaviour>();

        vacuumBehaviour.SetVacuumDetails(currentStats);
        isReady = false;
    }
}
