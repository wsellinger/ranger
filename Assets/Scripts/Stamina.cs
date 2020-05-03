using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public uint MaxStamina;
    public uint LossPerInterval;
    public uint Interval;

    private uint uiCurrentStamina;

    float fTimer;

    public uint CurrentStamina
    {
        get
        {
            return uiCurrentStamina;
        }
    }

    public float StaminaPercent
    {
        get
        {
            float fTest= (float)uiCurrentStamina / (float)MaxStamina;
            return fTest;
        }
    }

    void Awake()
    {
        uiCurrentStamina = MaxStamina;
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
            uiCurrentStamina = (uint)Mathf.Max(CurrentStamina - LossPerInterval, 0);
            fTimer = 0;
        }
    }
}
