using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitality : Stat
{
	public float DecayPerSec;
	public float RestPerSec;

	private Health m_health;
	private Stamina m_stamina;

	public float VitalityPercent
	{
		get
		{
			return CurrentStat / (float)MaxStat;
		}
	}
	public override void Awake()
	{
		base.Awake();

		m_health = GetComponent<Health>();
		m_stamina = GetComponent<Stamina>();
	}

	public void Replenish(float fValue)
	{
		CurrentStat += Mathf.Abs(fValue);
	}

	private void Update()
	{
		CurrentStat -= DecayPerSec * Time.deltaTime;

		if (Input.GetButton("Rest"))
		{
			bool bStatsToReplinish = !m_health.MaxFull || !m_stamina.MaxFull;
			if (bStatsToReplinish && !MaxEmpty)
			{
				float fRest = RestPerSec * Time.deltaTime;
				m_health.RegenerateStat(fRest);
				m_stamina.RegenerateStat(fRest);
				CurrentMaxStat -= fRest;
			}
		}
	}
}
