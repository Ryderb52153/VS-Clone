using System;
using UnityEngine;

public class VacuumBehaviour : MonoBehaviour
{
    private float cooldown = 50f;
    private Vector3 direction;
    private float speed;
    private float damage;
    private float lifeSpan;

    private float timeRemaining;

    private void OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        timeRemaining--;

        if (timeRemaining < 0)
        {
            timeRemaining = lifeSpan;
            this.gameObject.SetActive(false);
        }
    }
}