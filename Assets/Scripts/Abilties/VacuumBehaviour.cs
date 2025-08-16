using System;
using System.Collections.Generic;
using UnityEngine;

public class VacuumBehaviour : MonoBehaviour
{
    private float holdRadius = 0.35f;
    private float pullSpeed = 4f;
    private float lifeSpan = 20f;

    public float SetHoldRadius { set => holdRadius = value; }
    public float SetPullSpeed { set => pullSpeed = value; }
    public float SetLifeSpane { set => lifeSpan = value; }
    public void SetRadius(float radius) { collider2d.radius = radius; }

    private float timeRemaining;
    private CircleCollider2D collider2d;
    private List<ExperienceDrop> nearbyExp;

    private void Awake()
    {
        collider2d = GetComponent<CircleCollider2D>();
        collider2d.isTrigger = true;
        nearbyExp = new List<ExperienceDrop>();
    }

    private void FixedUpdate()
    {
        foreach (ExperienceDrop exp in new List<ExperienceDrop>(nearbyExp))
        {
            if (!exp.gameObject.activeInHierarchy)
            {
                nearbyExp.Remove(exp);
                continue;
            }

            float distance = Vector2.Distance(exp.transform.position, transform.position);

            if (distance >= holdRadius)
            {
                Vector2 next = Vector2.MoveTowards(exp.transform.position, transform.position, pullSpeed * Time.fixedDeltaTime);
                exp.transform.position = next;
            }
        }
    }

    private void OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        timeRemaining--;

        if (timeRemaining < 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Experience") { return; }

        if (collision.TryGetComponent<ExperienceDrop>(out ExperienceDrop experience))
        {
            nearbyExp.Add(experience);
        }
    }

    private void OnEnable()
    {
        timeRemaining = lifeSpan;
        TimeTickSystem.OnTick += OnTick;
    }

    private void OnDisable()
    {
        timeRemaining = lifeSpan;
        nearbyExp.Clear();
        TimeTickSystem.OnTick -= OnTick;
    }
}