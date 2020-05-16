using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : Stat
{
    public float MaxRegenPerSec;

    private Vitality m_vitality;

    override public void Awake()
    {
        base.Awake();

        m_vitality = GetComponent<Vitality>();
	}

    void Update()
	{
        //Regen
		CurrentStat += MaxRegenPerSec * Time.deltaTime * m_vitality.VitalityPercent;
	}
}
