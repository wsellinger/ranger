using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Health : Stat
{
	[Tooltip("This is the fastest health can regen. Actual regen is a factor of remaining vitality.")]
	public float MaxRegenPerSec = 2;
	[Tooltip("This is the percent of leftover damage that impacts max health when taking damage with no life.")]
	public float DamageOverflowModifier = 0.5f;

	private Vitality m_vitality;

	override public void Awake()
	{
		base.Awake();

		m_vitality = GetComponent<Vitality>();
	}

	void Update()
	{
		//Regen
		if (!Full)
		{
			CurrentStat += MaxRegenPerSec * Time.deltaTime * m_vitality.VitalityPercent;
		}
	}

	public void TakeDamage(float fDamage)
	{
		float fOverflowDamage = GetOverflowDamage(fDamage);
		CurrentStat -= fDamage;
		CurrentMaxStat -= fOverflowDamage;
	}

	public void Heal(float fHeal)
	{
		CurrentStat += fHeal;
	}

	private float GetOverflowDamage(float fDamage)
	{
		fDamage -= CurrentStat;
		return fDamage > 0 ? fDamage * DamageOverflowModifier : 0;
	}
}
