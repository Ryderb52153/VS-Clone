using UnityEngine;

public class VacuumPU : Ability
{
    private bool isReady = false;
    private Camera myCamera;

    private void Awake()
    {
        myCamera = Camera.main;
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

        // Spawn a object at the mouse position
        Vector3 mousePosition = myCamera.ScreenToWorldPoint(Input.mousePosition);
        GameObject gameObject = ObjectPooler.Instance.SpawnFromPool("Vacuum Ability", mousePosition, transform.rotation);

        isReady = false;
    }
}
