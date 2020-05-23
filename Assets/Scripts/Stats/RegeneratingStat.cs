using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegeneratingStat : Stat
{
	[Tooltip("The maximum amount of stat regen per second. Full regen is a factor of remaining vitality.")]
	public float MaxRegenPerSec;

	[Tooltip("This is the percent of leftover damage that impacts max stat when damaging an empty stat.")]
	public float OverflowModifier = 0.5f;

	[Tooltip("The amount of time in seconds to wait before stamina regen continues after loss")]
	public float RegenDelay;

	protected float m_fRegenDelay;

	private Vitality m_vitality;

	public override void Awake()
	{
		base.Awake();

		m_vitality = GetComponent<Vitality>();
	}

	public void DamageStat(float fDamage)
	{
		fDamage = Mathf.Abs(fDamage);

		float fOverflowDamage = GetOverflowDamage(fDamage);
		CurrentStat -= fDamage;
		CurrentMaxStat -= fOverflowDamage;

		m_fRegenDelay = RegenDelay;
	}

	virtual protected void Update()
	{
		if (m_fRegenDelay != 0)
		{
			m_fRegenDelay = Mathf.Max(m_fRegenDelay - Time.deltaTime, 0);
		}
		else
		{
			Regen();
		}
	}

	private float GetOverflowDamage(float fDamage)
	{
		fDamage -= CurrentStat;
		return fDamage > 0 ? fDamage * OverflowModifier : 0;
	}

	virtual protected void Regen()
	{
		if (!Full)
		{
			CurrentStat += MaxRegenPerSec * Time.deltaTime * m_vitality.VitalityPercent;
		}
	}
}
