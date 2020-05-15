using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaHealthRegen : MonoBehaviour 
{
    public uint MaxRegen;
    public uint Interval;
    
    Health hHealth;
    Stamina sStamina;
    float fTimer;

    void Awake ()
    {
        hHealth = GetComponent<Health>();
        sStamina = GetComponent<Stamina>();

        fTimer = 0;
    }
	
	void Update () 
	{
        UpdateTimer();
    }

    void UpdateTimer()
    {
        if (fTimer < Interval)
        {
            fTimer = Mathf.Min(fTimer + Time.deltaTime, Interval);
        }

        if (fTimer >= Interval)
        {
            OnTimerInterval();
            fTimer = 0;
        }
    }

    void OnTimerInterval()
    {
        uint uiHealAmount = (uint)Mathf.CeilToInt(MaxRegen * sStamina.StaminaPercent);
        hHealth.Heal(uiHealAmount);
    }
}
