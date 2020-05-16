using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Health : Stat
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

	public void TakeDamage(float uiDamageAmount)
	{
		CurrentStat -= uiDamageAmount;
	}

	public void Heal(float uiHealAmount)
	{
		CurrentStat += uiHealAmount;
	}
}
