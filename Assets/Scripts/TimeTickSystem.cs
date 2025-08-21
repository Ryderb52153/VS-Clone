using System;
using UnityEngine;

public class TimeTickSystem : MonoBehaviour
{
    public static event EventHandler<OnTickEventArgs> OnTick;

    private const float TICK_TIMER_MAX = 1f;

    public class OnTickEventArgs : EventArgs
    {
        public int tick;
    }

    private int tick;
    private float tickTimer;

    private void Awake()
    {
        tick = 0;
    }

    private void Update()
    {
        tickTimer += Time.deltaTime;

        if (tickTimer >= TICK_TIMER_MAX)
        {
            tickTimer -= TICK_TIMER_MAX;
            tick++;

            if (OnTick != null) OnTick(this, new OnTickEventArgs { tick = tick});
        }
    }
}


