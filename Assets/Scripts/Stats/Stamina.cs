using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : Stat
{
    public uint LossPerInterval;
    public uint Interval;

    float fTimer;

    public float StaminaPercent
    {
        get
        {
            return (float)CurrentStat / (float)MaxStat;
        }
    }

    override public void Awake()
    {
        base.Awake();
        fTimer = 0;
    }

    void Update ()
    {
        if (fTimer < Interval)
        {
            fTimer = Mathf.Min(fTimer + Time.deltaTime, Interval);
        }

        if (fTimer >= Interval)
        {
            CurrentStat = (uint)Math.Max(CurrentStat - LossPerInterval, 0);
            fTimer = 0;
        }
    }
}
